namespace AvHModHelper.Patches.Currency;

using Currency = global::Currency;

[HarmonyPatch(typeof(Currency), nameof(Currency.CashBack))]
internal static class Currency_CashBack
{
    [HarmonyPrefix]
    internal static bool Prefix(ref Currency __instance)
    {
        var result = true;
        var unref = __instance;
        Helper.PerformHook(mod => result &= mod.PreSell(ref unref));
        __instance = unref;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(Currency __instance)
    {
        Helper.PerformHook(mod => mod.PostSell(__instance));
    }
}
