﻿namespace AvHModHelper.Patches.Currency;

using Currency = global::Currency;

[HarmonyPatch(typeof(Currency), "Start")]
internal static class Currency_Cashback
{
    [HarmonyPrefix]
    internal static bool Prefix(Currency __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreCashLoaded(__instance));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(Currency __instance)
    {
        Helper.PerformHook(mod => mod.PostCashLoaded(__instance));
    }
}