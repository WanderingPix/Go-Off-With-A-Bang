using System.Collections;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace GoOffWithABang.Cutscenes;

public class Swoon : CustomDeathCutscene
{
    public override bool HideNormalKillCutscene => false;

    public override IEnumerator Trigger()
    {
        var rend = UnityObject.Instantiate(HudManager.Instance.FullScreen, HudManager.Instance.transform);
        rend.sprite = new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.swoon.png", 1080).LoadAsset();
        rend.color = Color.white;
        rend.gameObject.SetActive(true);
        SoundManager.Instance.PlaySound(
            new LoadableAudioResourceAsset("GoOffWithABang.Resources.Sounds.swoon.wav").LoadAsset(), false);
        yield return new WaitForSeconds(2.75f);
        rend.gameObject.Destroy();
    }
}