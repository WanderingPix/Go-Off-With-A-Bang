using System.Collections;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace GoOffWithABang.Cutscenes;

public class toby : CustomDeathCutscene
{
    public override bool HideNormalKillCutscene => true;

    public override IEnumerator Trigger()
    {
        Logger.GlobalInstance.Error("toby MOTHERFUCKING FOX");
        var obj = new GameObject("tran");
        obj.transform.position = PlayerControl.LocalPlayer.transform.position;
        obj.transform.SetParent(HudManager.Instance.transform);
        obj.layer = LayerMask.NameToLayer("UI");
        var rend = obj.AddComponent<SpriteRenderer>();
        rend.sprite = new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.dog.png").LoadAsset();
        yield return HudManager.Instance.StartCoroutine(Effects.Slide2D(obj.transform, new Vector2(20, 0), new Vector2(-20, 0), 0.7f));
        obj.Destroy();
    }
}