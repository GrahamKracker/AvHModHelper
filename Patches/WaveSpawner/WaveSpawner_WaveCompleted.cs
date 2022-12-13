namespace AvHModHelper.Patches.WaveSpawner;

[HarmonyPatch(typeof(global::WaveSpawner), "WaveCompleted")]
internal static class WaveSpawner_WaveCompleted
{
    [HarmonyPostfix]
    internal static void Postfix(global::WaveSpawner __instance)
    {
        Helper.PerformHook(mod => mod.OnWaveCompleted(__instance));
    }
}