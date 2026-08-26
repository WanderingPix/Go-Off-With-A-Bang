using System;
using System.Collections;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GoOffWithABang.Cutscenes;

public class GD : CustomDeathCutscene
{
    public override bool HideNormalKillCutscene => true;

    public override IEnumerator Trigger()
    {
        SoundManager.Instance.PlaySound(
            new LoadableAudioResourceAsset("GoOffWithABang.Resources.Sounds.gd.wav").LoadAsset(), false);
        var circle = new GameObject().AddComponent<SpriteRenderer>();
        circle.transform.position = PlayerControl.LocalPlayer.transform.position;
        circle.sprite = new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.circle.png", 128).LoadAsset();
        PlayerControl.LocalPlayer.StartCoroutine(Effects.ScaleIn(circle.transform, 0, 2, 1));
        for (int i = 0; i < 10; i++)
        {
            var square = new GameObject().AddComponent<SpriteRenderer>();
            square.transform.position = PlayerControl.LocalPlayer.transform.position;
            square.sprite = new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.square.png", 128).LoadAsset();
            float angle = Random.RandomRange(0f, 2f * (float)Math.PI);
            PlayerControl.LocalPlayer.StartCoroutine(Effects.Slide2D(square.transform, PlayerControl.LocalPlayer.transform.position, PlayerControl.LocalPlayer.transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)), 0.9f));
            PlayerControl.LocalPlayer.StartCoroutine(Effects.ColorFade(square, PlayerControl.LocalPlayer.Data.Color, Color.clear, 0.7f));
            PlayerControl.LocalPlayer.StartCoroutine(Effects.ScaleIn(square.transform, Random.RandomRange(0.1f, 0.3f), 0, 0.6f));
        }
        yield return PlayerControl.LocalPlayer.StartCoroutine(Effects.ColorFade(circle, PlayerControl.LocalPlayer.Data.Color, Color.clear, 0.7f));
        circle.gameObject.Destroy();
    }
}