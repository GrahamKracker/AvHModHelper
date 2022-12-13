namespace AvHModHelper.Patches.WaveSpawner;

[HarmonyPatch(typeof(global::WaveSpawner), "Update")]
internal static class WaveSpawner_Update
{
    [HarmonyPrefix]
    internal static bool Prefix(ref global::WaveSpawner __instance)
    {
        var result = true;
        var unref = __instance;
        Helper.PerformHook(mod => result &= mod.PreWaveUpdate(ref unref));
        __instance = unref;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(global::WaveSpawner __instance)
    {
        Helper.PerformHook(mod => mod.PostWaveUpdate(__instance));
    }
}
