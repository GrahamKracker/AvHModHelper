namespace AvHModHelper.Patches.Currency;

[HarmonyPatch(typeof(global::Currency), nameof(global::Currency.CashBack))]
internal static class Currency_CashBack
{
    [HarmonyPrefix]
    internal static bool Prefix(ref global::Currency __instance)
    {
        var result = true;
        var unref = __instance;
        Helper.PerformHook(mod => result &= mod.PreSell(ref unref));
        __instance = unref;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(global::Currency __instance)
    {
        Helper.PerformHook(mod => mod.PostSell(__instance));
    }
}
