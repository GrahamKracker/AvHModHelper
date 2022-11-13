namespace AvHModHelper.Patches.Currency;

using Currency = global::Currency;

[HarmonyPatch(typeof(Currency), "Start")]
internal static class Currency_Start
{
    [HarmonyPrefix]
    internal static bool Prefix(ref Currency __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreCashLoaded(ref __instance));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(ref Currency __instance)
    {
        Helper.PerformHook(mod => mod.PostCashLoaded(ref __instance));
    }
}
