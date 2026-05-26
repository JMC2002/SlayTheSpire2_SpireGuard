using JmcModLib.Config;
using JmcModLib.Config.UI;
using JmcModLib.Utils;

namespace SpireGuard.Core;

public static class SpireGuardSettings
{
    private const string GeneralGroup = "general";

    [UIToggle]
    [Config(
        "启用 SpireGuard",
        onChanged: nameof(OnEnabledChanged),
        group: GeneralGroup,
        Description = "开启后启用联机控制台保护；关闭后补丁会直接放行原版逻辑。",
        Key = "enabled",
        Order = 10)]
    public static bool Enabled = true;

    [UIToggle]
    [Config(
        "客机时禁止本机控制台",
        onChanged: nameof(OnBlockLocalClientConsoleChanged),
        group: GeneralGroup,
        Description = "开启后进入联机运行且本机作为客机时，无法主动打开或提交控制台命令；默认关闭。",
        Key = "block_local_client_console",
        Order = 20)]
    public static bool BlockLocalClientConsole = false;

    [UIToggle]
    [Config(
        "检测其他玩家控制台",
        onChanged: nameof(OnShowRemoteConsolePopupChanged),
        group: GeneralGroup,
        Description = "开启后检测到其他玩家的控制台网络动作时，会弹出单按钮提示框显示玩家名称。",
        Key = "show_remote_console_popup",
        Order = 30)]
    public static bool ShowRemoteConsolePopup = true;

    [UIToggle]
    [Config(
        "主机拦截时弹出提示",
        onChanged: nameof(OnHostBlockPopupChanged),
        group: GeneralGroup,
        Description = "开启后主机拦截客机控制台网络动作时，会弹出单按钮提示框显示玩家名称。",
        Key = "show_host_block_popup",
        Order = 40)]
    public static bool ShowHostBlockPopup = true;

    private static void OnEnabledChanged(bool enabled)
    {
        ModLogger.Info($"SpireGuard 已{(enabled ? "启用" : "停用")}。");
    }

    private static void OnBlockLocalClientConsoleChanged(bool enabled)
    {
        ModLogger.Info($"SpireGuard 客机本机控制台限制已{(enabled ? "启用" : "停用")}。");
    }

    private static void OnShowRemoteConsolePopupChanged(bool enabled)
    {
        ModLogger.Info($"SpireGuard 其他玩家控制台提醒已{(enabled ? "启用" : "停用")}。");
    }

    private static void OnHostBlockPopupChanged(bool enabled)
    {
        ModLogger.Info($"SpireGuard 主机拦截弹窗已{(enabled ? "启用" : "停用")}。");
    }
}
