namespace AvHModHelper.Patches.Currency;

using Currency = global::Currency;

[HarmonyPatch(typeof(Currency), nameof(Currency.UpdateCurrency))]
internal static class Currency_UpdateCurrency
{
    [HarmonyPrefix]
    internal static bool Prefix(ref Currency __instance, ref int amount, ref bool canDouble)
    {
        var result = true;
        var unrefamount = amount;
        var unrefcanDouble = canDouble;
        Helper.PerformHook(mod => result &= mod.PreCashAdded(ref __instance, ref unrefamount, ref unrefcanDouble));
        amount = unrefamount;
        canDouble = unrefcanDouble;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(ref Currency __instance, ref int amount, ref bool canDouble)
    {
        Helper.PerformHook(mod => mod.PostCashAdded(ref __instance, ref amount, ref canDouble));
    }
}
