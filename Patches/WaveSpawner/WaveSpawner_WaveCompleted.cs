﻿namespace AvHModHelper.Patches.WaveSpawner;

using WaveSpawner = global::WaveSpawner;

[HarmonyPatch(typeof(WaveSpawner), "WaveCompleted")]
internal static class WaveSpawner_WaveCompleted
{
    [HarmonyPostfix]
    internal static void Postfix(WaveSpawner __instance)
    {
        Helper.PerformHook(mod => mod.OnWaveCompleted(__instance));
    }
}
