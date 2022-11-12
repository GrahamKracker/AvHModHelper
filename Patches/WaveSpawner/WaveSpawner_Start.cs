namespace AvHModHelper.Patches.WaveSpawner;

using WaveSpawner = global::WaveSpawner;

[HarmonyPatch(typeof(WaveSpawner), "Start")]
internal static class WaveSpawner_Start
{
    [HarmonyPostfix]
    public static void Postfix(WaveSpawner __instance)
    {
        Helper.PerformHook(mod => mod.OnWaveSpawnerInit(__instance));
    }
}
