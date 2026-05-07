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

    private static void OnEnabledChanged(bool enabled)
    {
        ModLogger.Info($"SpireGuard 已{(enabled ? "启用" : "停用")}。");
    }
}
