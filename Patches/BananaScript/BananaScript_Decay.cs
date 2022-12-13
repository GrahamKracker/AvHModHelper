namespace AvHModHelper.Patches.BananaScript;

[HarmonyPatch(typeof(global::BananaScript), "Decay")]
internal static class BananaScript_Decay
{
    [HarmonyPrefix]
    internal static bool Prefix(ref global::BananaScript __instance)
    {
        var result = true;
        var unref = __instance;
        Helper.PerformHook(mod => result &= mod.PreBananaDecay(ref unref));
        __instance = unref;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(global::BananaScript __instance)
    {
        Helper.PerformHook(mod => mod.PostBananaDecay(__instance));
    }
}
