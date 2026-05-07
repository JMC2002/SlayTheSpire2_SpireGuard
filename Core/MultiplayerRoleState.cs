using JmcModLib.Utils;
using MegaCrit.Sts2.Core.Multiplayer.Game;
using MegaCrit.Sts2.Core.Runs;

namespace SpireGuard.Core;

public static class MultiplayerRoleState
{
    public const string GuestBlockedMessage = "联机客机不能使用控制台。";

    private static INetGameService? lobbyNetService;

    public static bool ShouldBlockLocalClientConsole()
    {
        return SpireGuardSettings.Enabled && GetCurrentRole().IsConnectedClient;
    }

    public static bool ShouldBlockRemoteConsoleActionOnHost()
    {
        return SpireGuardSettings.Enabled && GetCurrentRole().IsConnectedHost;
    }

    public static void TrackLobbyService(INetGameService? netService, string source)
    {
        lobbyNetService = netService;

        if (netService?.Type.IsMultiplayer() == true)
        {
            ModLogger.Debug($"SpireGuard 已记录{source}网络状态：{netService.Type}。");
        }
    }

    public static void ClearLobbyService(INetGameService? netService, string source)
    {
        if (netService == null || ReferenceEquals(lobbyNetService, netService))
        {
            lobbyNetService = null;
            ModLogger.Debug($"SpireGuard 已清理{source}网络状态缓存。");
        }
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

        return lobbyNetService;
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
