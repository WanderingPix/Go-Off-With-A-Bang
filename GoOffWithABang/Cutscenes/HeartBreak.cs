using System.Collections;
using System.Collections.Generic;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace GoOffWithABang.Cutscenes;

public class HeartBreak : CustomDeathCutscene
{
    public override bool HideNormalKillCutscene => true;
    public List<LoadableResourceAsset> Pieces = [
        new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.SoulBreaking.1.png"),
        new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.SoulBreaking.2.png"),
        new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.SoulBreaking.3.png"),
        new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.SoulBreaking.4.png"),
        new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.SoulBreaking.5.png"),
        new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.SoulBreaking.6.png")
    ];
    public override IEnumerator Trigger()
    {
        SoundManager.Instance.PlaySound(
            new LoadableAudioResourceAsset("GoOffWithABang.Resources.Sounds.soul_break.wav").LoadAsset(), false);
        var background = UnityObject.Instantiate(HudManager.Instance.FullScreen, HudManager.Instance.transform);
        background.sprite = new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.black.png", 1080).LoadAsset();
        background.color = Color.white;
        background.gameObject.SetActive(true);
        var soulGo = new GameObject("Broken Soul");
        var soulRenderer = soulGo.AddComponent<SpriteRenderer>();
        soulRenderer.sprite = new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.SoulBreaking.soul.png").LoadAsset();
        soulGo.layer = LayerMask.NameToLayer("UI");
        soulRenderer.material = new Material(HatManager.Instance.PlayerMaterial);
        PlayerMaterial.SetColors(PlayerControl.LocalPlayer.cosmetics.ColorId, soulRenderer);
        soulGo.transform.parent = background.transform;
        soulGo.transform.localPosition = Vector3.zero - new Vector3(0, 0, 10);
        background.material = new Material(HatManager.Instance.PlayerMaterial);
        PlayerMaterial.SetColors(PlayerControl.LocalPlayer.cosmetics.ColorId, background);
        yield return new WaitForSeconds(1.5f);
        background.sprite = new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.black.png", 1080).LoadAsset();
        soulGo.gameObject.Destroy();
        foreach (var piece in Pieces)
        {
            var go = new GameObject("Broken Soul");
            var spriteRenderer = go.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = piece.LoadAsset();
            go.layer = LayerMask.NameToLayer("UI");
            spriteRenderer.material = new Material(HatManager.Instance.PlayerMaterial);
            PlayerMaterial.SetColors(PlayerControl.LocalPlayer.cosmetics.ColorId, spriteRenderer);
            go.transform.parent = background.transform;
            go.transform.localPosition = Vector3.zero - new Vector3(0, 0, 10);
            var body = go.AddComponent<Rigidbody2D>();
            body.freezeRotation = false;
            body.drag = 1.5f;
            body.centerOfMass = Vector2.left;
            body.gravityScale = 1;
            body.AddForce(new Vector2(Random.RandomRange(-2.5f, 2.5f), 5), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(2f);
        background.gameObject.Destroy();
    }
}