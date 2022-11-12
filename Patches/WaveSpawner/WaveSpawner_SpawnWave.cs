namespace AvHModHelper.Patches.WaveSpawner;

using WaveSpawner = global::WaveSpawner;

[HarmonyPatch(typeof(WaveSpawner), "SpawnWave")]
internal static class WaveSpawner_SpawnWave
{
    [HarmonyPostfix]
    internal static void Postfix(WaveSpawner __instance, WaveSpawner.Wave _wave)
    {
        Helper.PerformHook(mod => mod.OnWaveStarted(__instance, _wave));
    }
}
