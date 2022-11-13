namespace AvHModHelper.Patches.WaveSpawner;

using WaveSpawner = global::WaveSpawner;

[HarmonyPatch(typeof(WaveSpawner), "Start")]
internal static class WaveSpawner_Start
{
    [HarmonyPrefix]
    public static void Prefix(ref WaveSpawner __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreWaveSpawnerInit(ref __instance));
        return result;
    }

    [HarmonyPostfix]
    public static void Postfix(ref WaveSpawner __instance)
    {
        Helper.PerformHook(mod => mod.PostWaveSpawnerInit(ref __instance));
    }
}