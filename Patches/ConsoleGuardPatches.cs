using System;
using HarmonyLib;
using JmcModLib.Utils;
using MegaCrit.Sts2.Core.DevConsole;
using MegaCrit.Sts2.Core.GameActions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Multiplayer.Messages.Game;
using MegaCrit.Sts2.Core.Nodes.Debug;
using SpireGuard.Core;
using CoreDevConsole = MegaCrit.Sts2.Core.DevConsole.DevConsole;

namespace SpireGuard.Patches;

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
        HostBlockPopupService.NotifyBlockedConsoleAction(senderId, consoleAction.cmd);
        return false;
    }
}

[HarmonyPatch(typeof(ActionQueueSynchronizer), "HandleActionEnqueuedMessage", new Type[] { typeof(ActionEnqueuedMessage), typeof(ulong) })]
internal static class ActionQueueSynchronizerHandleActionEnqueuedMessagePatch
{
    [HarmonyPrefix]
    private static bool Prefix(ActionEnqueuedMessage message)
    {
        if (message.action is NetConsoleCmdGameAction consoleAction)
        {
            HostBlockPopupService.NotifyRemoteConsoleAction(message.playerId, consoleAction.cmd);
        }

        return true;
    }
}
