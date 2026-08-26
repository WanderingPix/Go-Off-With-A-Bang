using System.Collections;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace GoOffWithABang.Cutscenes;

public class tran : CustomDeathCutscene
{
    public override bool HideNormalKillCutscene => true;

    public override IEnumerator Trigger()
    {
        SoundManager.Instance.PlaySound(GameManagerCreator.Instance.HideAndSeekManagerPrefab.FinalHideAlertSFX, false);
        Logger.GlobalInstance.Error("trans");
        var obj = new GameObject("tran");
        obj.transform.position = PlayerControl.LocalPlayer.transform.position;
        obj.transform.SetParent(HudManager.Instance.transform);
        obj.layer = LayerMask.NameToLayer("UI");
        obj.transform.localScale = Vector3.one * 5;
        var rend = obj.AddComponent<SpriteRenderer>();
        rend.sprite = new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.tran.png").LoadAsset();
        yield return HudManager.Instance.StartCoroutine(Effects.Slide2D(obj.transform, new Vector2(-100, 0), new Vector2(100, 0), 0.7f));
        obj.Destroy();
    }
}