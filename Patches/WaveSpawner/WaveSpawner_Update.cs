namespace AvHModHelper.Patches.WaveSpawner;

using WaveSpawner = global::WaveSpawner;

[HarmonyPatch(typeof(WaveSpawner), "Update")]
internal static class WaveSpawner_Update
{
    [HarmonyPrefix]
    internal static bool Prefix(ref WaveSpawner __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreWaveUpdate(ref __instance));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(ref WaveSpawner __instance)
    {
        Helper.PerformHook(mod => mod.PostWaveUpdate(ref __instance));
    }
}