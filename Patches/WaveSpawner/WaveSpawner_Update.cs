namespace AvHModHelper.Patches.WaveSpawner;

using WaveSpawner = global::WaveSpawner;

[HarmonyPatch(typeof(WaveSpawner), "Update")]
internal static class WaveSpawner_Update
{
    [HarmonyPrefix]
    internal static bool Prefix(ref WaveSpawner __instance)
    {
        var result = true;
        var unref = __instance;
        Helper.PerformHook(mod => result &= mod.PreWaveUpdate(ref unref));
        __instance = unref;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(WaveSpawner __instance)
    {
        Helper.PerformHook(mod => mod.PostWaveUpdate(__instance));
    }
}
