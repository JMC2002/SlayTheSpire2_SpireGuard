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
        Description = "开启后在联机时禁止客机主动打开或提交控制台；关闭后补丁会直接放行原版逻辑。",
        Key = "enabled",
        Order = 10)]
    public static bool Enabled = true;

    [UIToggle]
    [Config(
        "主机拦截时弹出提示",
        onChanged: nameof(OnHostBlockPopupChanged),
        group: GeneralGroup,
        Description = "开启后主机拦截客机控制台网络动作时，会弹出单按钮提示框显示玩家名称。",
        Key = "show_host_block_popup",
        Order = 20)]
    public static bool ShowHostBlockPopup = true;

    private static void OnEnabledChanged(bool enabled)
    {
        ModLogger.Info($"SpireGuard 已{(enabled ? "启用" : "停用")}。");
    }

    private static void OnHostBlockPopupChanged(bool enabled)
    {
        ModLogger.Info($"SpireGuard 主机拦截弹窗已{(enabled ? "启用" : "停用")}。");
    }
}
