using System.Collections;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace GoOffWithABang.Cutscenes;

public class YouDied : CustomDeathCutscene
{
    public override bool HideNormalKillCutscene => true;

    public override IEnumerator Trigger()
    {
        var rend = UnityObject.Instantiate(HudManager.Instance.FullScreen, HudManager.Instance.transform);
        rend.sprite = new LoadableResourceAsset("GoOffWithABang.Resources.Overlays.youdied.png", 1080).LoadAsset();
        rend.gameObject.SetActive(true);
        rend.GetComponent<FullScreenScaler>().Destroy();
        rend.transform.localScale = Vector3.one * 6;
        yield return HudManager.Instance.StartCoroutine(Effects.ColorFade(rend, Color.clear, Color.white, 2));
        rend.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        yield return HudManager.Instance.StartCoroutine(Effects.ColorFade(rend, Color.white, Color.clear, 1));
        rend.gameObject.Destroy();
    }
}