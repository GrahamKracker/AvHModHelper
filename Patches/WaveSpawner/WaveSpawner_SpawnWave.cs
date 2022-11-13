namespace AvHModHelper.Patches.WaveSpawner;

using WaveSpawner = global::WaveSpawner;

[HarmonyPatch(typeof(WaveSpawner), "SpawnWave")]
internal static class WaveSpawner_SpawnWave
{
    [HarmonyPostfix]
    internal static void Postfix(ref WaveSpawner __instance, ref WaveSpawner.Wave _wave)
    {
        Helper.PerformHook(mod => mod.OnWaveStarted(ref __instance, ref _wave));
    }
}