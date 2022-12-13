namespace AvHModHelper.Patches.PlayerHealth;

[HarmonyPatch(typeof(global::PlayerHealth), nameof(global::PlayerHealth.UpdateHealth))]
internal static class PlayerHealth_UpdateHealth
{
    [HarmonyPrefix]
    internal static bool Prefix(ref global::PlayerHealth __instance, ref int amount)
    {
        var result = true;
        var unrefamount = amount;
        var unref = __instance;
        Helper.PerformHook(mod => result &= mod.PreHealthAdded(ref unref, ref unrefamount));
        amount = unrefamount;
        __instance = unref;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(global::PlayerHealth __instance, int amount)
    {
        Helper.PerformHook(mod => mod.PostHealthAdded(__instance, amount));
    }
}
