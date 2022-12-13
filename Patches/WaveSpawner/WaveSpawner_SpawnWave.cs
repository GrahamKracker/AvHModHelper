namespace AvHModHelper.Patches.WaveSpawner;

[HarmonyPatch(typeof(global::WaveSpawner), "SpawnWave")]
internal static class WaveSpawner_SpawnWave
{
    [HarmonyPostfix]
    internal static void Postfix(global::WaveSpawner __instance, global::WaveSpawner.Wave _wave)
    {
        Helper.PerformHook(mod => mod.OnWaveStarted(__instance, _wave));
    }
}