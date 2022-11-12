namespace AvHModHelper.Patches.Enemy;

[HarmonyPatch(typeof(Currency), nameof(Currency.UpdateCurrency))]
internal static class Currency_UpdateCurrency
{
    [HarmonyPrefix]
    internal static bool Prefix(Currency __instance, ref int amount, ref bool canDouble)
    {
        var result = true;
        var unrefamount = amount;
        var unrefcanDouble = canDouble;
        Helper.PerformHook(mod => result &= mod.PreCashAdded(__instance, ref unrefamount, ref unrefcanDouble));
        amount = unrefamount;
        canDouble = unrefcanDouble;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(Currency __instance, int amount, bool canDouble)
    {
        Helper.PerformHook(mod => mod.PostCashAdded(__instance, amount, canDouble));
    }
}
