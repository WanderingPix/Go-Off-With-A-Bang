using BepInEx.Unity.IL2CPP.Utils.Collections;
using HarmonyLib;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;

namespace GoOffWithABang.Patches;

[HarmonyPatch]
public class KillCutscenePatch
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
    [HarmonyPostfix]
    public static void ShowKillAnimation(PlayerControl __instance, ref PlayerControl target, ref MurderResultFlags resultFlags)
    {
        if (!target.AmOwner) return;
        var customDeathCutscene = GoOffWithABangPlugin.Cutscenes.Random();
        SoundManager.Instance.StopAllSound();
        Coroutines.Start(customDeathCutscene.Trigger());
        if (customDeathCutscene.HideNormalKillCutscene)
        {
            HudManager.Instance.KillOverlay.gameObject.SetActive(false);
            HudManager.Instance.KillOverlay.queue.Clear();
            HudManager.Instance.KillOverlay.showAll = null;
        }
    }
}