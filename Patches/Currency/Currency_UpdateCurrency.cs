namespace AvHModHelper.Patches.Currency;

[HarmonyPatch(typeof(global::Currency), nameof(global::Currency.UpdateCurrency))]
internal static class Currency_UpdateCurrency
{
    [HarmonyPrefix]
    internal static bool Prefix(ref global::Currency __instance, ref int amount, ref bool canDouble)
    {
        var result = true;
        var unrefamount = amount;
        var unrefcanDouble = canDouble;
        var unref = __instance;
        Helper.PerformHook(mod => result &= mod.PreCashAdded(ref unref, ref unrefamount, ref unrefcanDouble));
        __instance = unref;
        amount = unrefamount;
        canDouble = unrefcanDouble;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(global::Currency __instance, int amount, bool canDouble)
    {
        Helper.PerformHook(mod => mod.PostCashAdded(__instance, amount, canDouble));
    }
}
