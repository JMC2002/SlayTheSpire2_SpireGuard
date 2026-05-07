using System;
using HarmonyLib;
using JmcModLib.Utils;
using MegaCrit.Sts2.Core.DevConsole;
using MegaCrit.Sts2.Core.GameActions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Multiplayer.Game;
using MegaCrit.Sts2.Core.Multiplayer.Game.Lobby;
using MegaCrit.Sts2.Core.Multiplayer.Messages.Game;
using MegaCrit.Sts2.Core.Nodes.Debug;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves;
using SpireGuard.Core;
using CoreDevConsole = MegaCrit.Sts2.Core.DevConsole.DevConsole;

namespace SpireGuard.Patches;

[HarmonyPatch(typeof(StartRunLobby), MethodType.Constructor, new Type[] { typeof(GameMode), typeof(INetGameService), typeof(IStartRunLobbyListener), typeof(int) })]
internal static class StartRunLobbyConstructorPatch
{
    [HarmonyPostfix]
    private static void Postfix(StartRunLobby __instance)
    {
        MultiplayerRoleState.TrackLobbyService(__instance.NetService, "新开局大厅");
    }
}

[HarmonyPatch(typeof(StartRunLobby), nameof(StartRunLobby.CleanUp))]
internal static class StartRunLobbyCleanUpPatch
{
    [HarmonyPostfix]
    private static void Postfix(StartRunLobby __instance, bool disconnectSession)
    {
        if (disconnectSession || !__instance.NetService.IsConnected)
        {
            MultiplayerRoleState.ClearLobbyService(__instance.NetService, "新开局大厅");
        }
    }
}

[HarmonyPatch(typeof(LoadRunLobby), MethodType.Constructor, new Type[] { typeof(INetGameService), typeof(ILoadRunLobbyListener), typeof(SerializableRun) })]
internal static class LoadRunLobbyConstructorPatch
{
    [HarmonyPostfix]
    private static void Postfix(LoadRunLobby __instance)
    {
        MultiplayerRoleState.TrackLobbyService(__instance.NetService, "读档大厅");
    }
}

[HarmonyPatch(typeof(LoadRunLobby), nameof(LoadRunLobby.CleanUp))]
internal static class LoadRunLobbyCleanUpPatch
{
    [HarmonyPostfix]
    private static void Postfix(LoadRunLobby __instance, bool disconnectSession)
    {
        if (disconnectSession || !__instance.NetService.IsConnected)
        {
            MultiplayerRoleState.ClearLobbyService(__instance.NetService, "读档大厅");
        }
    }
}

[HarmonyPatch(typeof(NDevConsole), nameof(NDevConsole.ShowConsole))]
internal static class NDevConsoleShowConsolePatch
{
    [HarmonyPrefix]
    private static bool Prefix(NDevConsole __instance)
    {
        if (!MultiplayerRoleState.ShouldBlockLocalClientConsole())
        {
            return true;
        }

        ModLogger.Debug("SpireGuard 已阻止联机客机打开控制台。");
        __instance.HideConsole();
        return false;
    }
}

[HarmonyPatch(typeof(CoreDevConsole), nameof(CoreDevConsole.ProcessCommand), new Type[] { typeof(string) })]
internal static class DevConsoleProcessCommandPatch
{
    [HarmonyPrefix]
    private static bool Prefix(string inputValue, ref CmdResult __result)
    {
        if (!MultiplayerRoleState.ShouldBlockLocalClientConsole())
        {
            return true;
        }

        string command = string.IsNullOrWhiteSpace(inputValue) ? "<空命令>" : inputValue.Trim();
        ModLogger.Warn($"SpireGuard 已阻止联机客机提交控制台命令：{command}");
        __result = new CmdResult(success: false, MultiplayerRoleState.GuestBlockedMessage);
        return false;
    }
}

[HarmonyPatch(typeof(ActionQueueSynchronizer), nameof(ActionQueueSynchronizer.RequestEnqueue), new Type[] { typeof(GameAction) })]
internal static class ActionQueueSynchronizerRequestEnqueuePatch
{
    [HarmonyPrefix]
    private static bool Prefix(GameAction action)
    {
        if (action is not ConsoleCmdGameAction consoleAction ||
            !MultiplayerRoleState.ShouldBlockLocalClientConsole())
        {
            return true;
        }

        ModLogger.Warn($"SpireGuard 已阻止联机客机发送控制台同步动作：{consoleAction.Cmd}");
        return false;
    }
}

[HarmonyPatch(typeof(ActionQueueSynchronizer), "HandleRequestEnqueueActionMessage", new Type[] { typeof(RequestEnqueueActionMessage), typeof(ulong) })]
internal static class ActionQueueSynchronizerHandleRequestEnqueueActionMessagePatch
{
    [HarmonyPrefix]
    private static bool Prefix(RequestEnqueueActionMessage message, ulong senderId)
    {
        if (message.action is not NetConsoleCmdGameAction consoleAction ||
            !MultiplayerRoleState.ShouldBlockRemoteConsoleActionOnHost())
        {
            return true;
        }

        ModLogger.Warn($"SpireGuard 已阻止客机 {senderId} 发来的控制台网络动作：{consoleAction.cmd}");
        return false;
    }
}
