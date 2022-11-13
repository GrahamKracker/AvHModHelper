namespace AvHModHelper.Patches.WaveSpawner;

using WaveSpawner = global::WaveSpawner;

[HarmonyPatch(typeof(WaveSpawner), "Update")]
internal static class WaveSpawner_Update
{
    [HarmonyPrefix]
    internal static bool Prefix(WaveSpawner __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreWaveUpdate(__instance));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(WaveSpawner __instance)
    {
        Helper.PerformHook(mod => mod.PostWaveUpdate(__instance));
    }
}