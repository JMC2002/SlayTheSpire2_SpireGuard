using JmcModLib.Prefabs;
using JmcModLib.Utils;
using MegaCrit.Sts2.Core.Multiplayer.Game;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.Runs;

namespace SpireGuard.Core;

public static class HostBlockPopupService
{
    private const string PopupTitle = "SpireGuard 拦截提示";
    private const string PopupOkText = "知道了";
    private const int MaxCommandPreviewLength = 160;

    private static bool popupActive;

    public static void NotifyBlockedConsoleAction(ulong senderId, string command)
    {
        if (!SpireGuardSettings.Enabled || !SpireGuardSettings.ShowHostBlockPopup)
        {
            return;
        }

        if (popupActive)
        {
            ModLogger.Debug("SpireGuard 已有拦截提示弹窗显示中，本次只记录日志不重复弹窗。");
            return;
        }

        string playerName = ResolvePlayerName(senderId);
        string safePlayerName = EscapeRichText(playerName);
        string safeCommand = EscapeRichText(TrimCommand(command));
        string body = $"已拦截玩家 [gold]{safePlayerName}[/gold] 发来的控制台网络动作。\n命令：[red]{safeCommand}[/red]";

        popupActive = true;
        _ = ShowPopupAsync(body);
    }

    private static async Task ShowPopupAsync(string body)
    {
        try
        {
            if (!JmcConfirmationPopup.IsAvailable)
            {
                ModLogger.Debug("SpireGuard 拦截提示弹窗暂不可用，本次只记录日志。");
                return;
            }

            await JmcConfirmationPopup.ShowMessageAsync(
                PopupTitle,
                body,
                PopupOkText,
                showBackstop: true,
                assembly: typeof(HostBlockPopupService).Assembly);
        }
        catch (Exception ex)
        {
            ModLogger.Warn("显示 SpireGuard 拦截提示弹窗失败。", ex);
        }
        finally
        {
            popupActive = false;
        }
    }

    private static string ResolvePlayerName(ulong senderId)
    {
        try
        {
            INetGameService netService = RunManager.Instance.NetService;
            string playerName = PlatformUtil.GetPlayerName(netService.Platform, senderId);
            if (!string.IsNullOrWhiteSpace(playerName))
            {
                return playerName.Trim();
            }
        }
        catch (Exception ex)
        {
            ModLogger.Warn($"读取客机 {senderId} 的玩家名称失败，将使用 NetId 作为弹窗显示名。", ex);
        }

        return $"NetId {senderId}";
    }

    private static string TrimCommand(string command)
    {
        string trimmed = string.IsNullOrWhiteSpace(command) ? "<空命令>" : command.Trim();
        if (trimmed.Length <= MaxCommandPreviewLength)
        {
            return trimmed;
        }

        return trimmed[..MaxCommandPreviewLength] + "...";
    }

    private static string EscapeRichText(string text)
    {
        return text
            .Replace("[", "［", StringComparison.Ordinal)
            .Replace("]", "］", StringComparison.Ordinal);
    }
}
