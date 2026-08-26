using System.Collections;

namespace GoOffWithABang;

public abstract class CustomDeathCutscene
{
    public abstract bool HideNormalKillCutscene { get; }
    public abstract IEnumerator Trigger();
}