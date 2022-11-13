namespace AvHModHelper.Patches.WaveSpawner;

using WaveSpawner = global::WaveSpawner;

[HarmonyPatch(typeof(WaveSpawner), "Start")]
internal static class WaveSpawner_Start
{
    [HarmonyPrefix]
    public static bool Prefix(WaveSpawner __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreWaveSpawnerInit(__instance));
        return result;
    }

    [HarmonyPostfix]
    public static void Postfix(WaveSpawner __instance)
    {
        Helper.PerformHook(mod => mod.PostWaveSpawnerInit(__instance));
    }
}