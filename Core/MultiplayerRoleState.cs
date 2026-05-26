using JmcModLib.Utils;
using MegaCrit.Sts2.Core.Multiplayer.Game;
using MegaCrit.Sts2.Core.Runs;

namespace SpireGuard.Core;

public static class MultiplayerRoleState
{
    public const string GuestBlockedMessage = "联机客机不能使用控制台。";

    public static bool ShouldBlockLocalClientConsole()
    {
        return SpireGuardSettings.Enabled &&
            SpireGuardSettings.BlockLocalClientConsole &&
            GetCurrentRole().IsConnectedClient;
    }

    public static bool ShouldBlockRemoteConsoleActionOnHost()
    {
        return SpireGuardSettings.Enabled && GetCurrentRole().IsConnectedHost;
    }

    private static MultiplayerRole GetCurrentRole()
    {
        INetGameService? service = GetActiveNetService();
        if (service == null)
        {
            return MultiplayerRole.None;
        }

        try
        {
            NetGameType type = service.Type;
            bool isConnected = service.IsConnected;

            return new MultiplayerRole(
                type,
                isConnected,
                isConnected && type == NetGameType.Client,
                isConnected && type.IsMultiplayer(),
                isConnected && type == NetGameType.Host);
        }
        catch (Exception ex)
        {
            ModLogger.Warn("读取联机状态失败，本次控制台保护判定将按未联机处理。", ex);
            return MultiplayerRole.None;
        }
    }

    private static INetGameService? GetActiveNetService()
    {
        RunManager runManager = RunManager.Instance;
        if (runManager.IsInProgress && !runManager.IsCleaningUp)
        {
            return runManager.NetService;
        }

        return null;
    }

    private readonly record struct MultiplayerRole(
        NetGameType Type,
        bool IsConnected,
        bool IsConnectedClient,
        bool IsConnectedMultiplayer,
        bool IsConnectedHost)
    {
        public static MultiplayerRole None { get; } = new(
            NetGameType.None,
            IsConnected: false,
            IsConnectedClient: false,
            IsConnectedMultiplayer: false,
            IsConnectedHost: false);
    }
}
