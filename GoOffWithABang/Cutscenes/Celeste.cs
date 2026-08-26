using System;
using System.Collections;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GoOffWithABang.Cutscenes;

public class Celeste : CustomDeathCutscene
{
    public override bool HideNormalKillCutscene => true;

    public override IEnumerator Trigger()
    {
        Logger.GlobalInstance.Error("Celeste");
        SoundManager.Instance.PlaySound(
            new LoadableAudioResourceAsset("GoOffWithABang.Resources.Sounds.madeline_death.wav").LoadAsset(), false);
        var obj = new GameObject("CelesteDeath");
        obj.transform.position = PlayerControl.LocalPlayer.transform.position;
        for (int i = 0; i < 8; i++)
        {
            Logger.GlobalInstance.Error("Celeste" + i);
            var square = new GameObject("Square").AddComponent<SpriteRenderer>();
            square.transform.SetParent(obj.transform);
            float angle = i * (360f / 8f);
            square.transform.localPosition = new Vector3((float)Math.Cos(angle), (float)Math.Sin(angle), 0);
            square.sprite = new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.circle.png", 128).LoadAsset();
            square.color = PlayerControl.LocalPlayer.Data.Color;
            PlayerControl.LocalPlayer.StartCoroutine(Effects.ScaleIn(square.transform, 1, 0, 0.6f));
        }
        Logger.GlobalInstance.Error("Celeste madeline");
        PlayerControl.LocalPlayer.StartCoroutine(Effects.Rotate2D(obj.transform, 0, 90, 0.9f));
        yield return
            PlayerControl.LocalPlayer.StartCoroutine(Effects.ScaleIn(obj.transform, 0.5f, 2, 1));
        obj.Destroy();
    }
}