namespace AvHModHelper.Patches.PlayerHealth;

using PlayerHealth = global::PlayerHealth;

[HarmonyPatch(typeof(PlayerHealth), nameof(PlayerHealth.UpdateHealth))]
internal static class PlayerHealth_UpdateHealth
{
    [HarmonyPrefix]
    internal static bool Prefix(ref PlayerHealth __instance, ref int amount)
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
    internal static void Postfix(PlayerHealth __instance, int amount)
    {
        Helper.PerformHook(mod => mod.PostHealthAdded(__instance, amount));
    }
}
