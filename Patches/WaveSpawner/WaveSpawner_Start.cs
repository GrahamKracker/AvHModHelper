namespace AvHModHelper.Patches.WaveSpawner;

[HarmonyPatch(typeof(global::WaveSpawner), "Start")]
internal static class WaveSpawner_Start
{
    [HarmonyPrefix]
    public static bool Prefix(global::WaveSpawner __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreWaveSpawnerInit(__instance));
        return result;
    }

    [HarmonyPostfix]
    public static void Postfix(global::WaveSpawner __instance)
    {
        Helper.PerformHook(mod => mod.PostWaveSpawnerInit(__instance));
    }
}