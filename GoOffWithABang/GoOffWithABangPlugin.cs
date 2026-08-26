using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Reactor;
using Reactor.Utilities;

namespace GoOffWithABang;

[BepInAutoPlugin]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
public partial class GoOffWithABangPlugin : BasePlugin
{
    public Harmony Harmony { get; } = new(Id);
    public static List<CustomDeathCutscene> Cutscenes = new();
    public override void Load()
    {
        Cutscenes.Clear();
        foreach (var cutscene in Assembly.GetAssembly(typeof(GoOffWithABangPlugin)).GetTypes().Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(CustomDeathCutscene))))
        {
            Cutscenes.Add(Activator.CreateInstance(cutscene) as CustomDeathCutscene);
        }
        Harmony.PatchAll();
    }
}