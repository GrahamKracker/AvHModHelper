namespace AvHModHelper.Patches.BananaScript;

using BananaScript = global::BananaScript;

[HarmonyPatch(typeof(BananaScript), "Decay")]
internal static class BananaScript_Decay
{
    [HarmonyPrefix]
    internal static bool Prefix(ref BananaScript __instance)
    {
        var result = true;
        var unref = __instance;
        Helper.PerformHook(mod => result &= mod.PreBananaDecay(ref unref));
        __instance = unref;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(BananaScript __instance)
    {
        Helper.PerformHook(mod => mod.PostBananaDecay(__instance));
    }
}
