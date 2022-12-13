namespace AvHModHelper.Patches.Currency;

[HarmonyPatch(typeof(global::Currency), "Start")]
internal static class Currency_Start
{
    [HarmonyPrefix]
    internal static bool Prefix(ref global::Currency __instance)
    {
        var result = true;
        var unref = __instance;
        Helper.PerformHook(mod => result &= mod.PreCashLoaded(ref unref));
        __instance = unref;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(global::Currency __instance)
    {
        Helper.PerformHook(mod => mod.PostCashLoaded(__instance));
    }
}
