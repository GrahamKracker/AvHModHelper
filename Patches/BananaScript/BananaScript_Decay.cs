namespace AvHModHelper.Patches.BananaScript;

using BananaScript = global::BananaScript;

[HarmonyPatch(typeof(BananaScript), "Decay")]
internal static class BananaScript_Decay
{
    [HarmonyPrefix]
    internal static bool Prefix(BananaScript __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreBananaDecay(__instance));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(BananaScript __instance)
    {
        Helper.PerformHook(mod => mod.PostBananaDecay(__instance));
    }
}