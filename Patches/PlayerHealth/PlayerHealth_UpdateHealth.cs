namespace AvHModHelper.Patches.Enemy;

[HarmonyPatch(typeof(PlayerHealth), nameof(PlayerHealth.UpdateHealth))]
internal static class PlayerHealth_UpdateHealth
{
    [HarmonyPrefix]
    internal static bool Prefix(PlayerHealth __instance, ref int amount)
    {
        var result = true;
        var unrefamount = amount;
        Helper.PerformHook(mod => result &= mod.PreHealthAdded(__instance, ref unrefamount));
        amount = unrefamount;

        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(PlayerHealth __instance, int amount)
    {
        Helper.PerformHook(mod => mod.PostHealthAdded(__instance, amount));
    }
}