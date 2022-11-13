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
        Helper.PerformHook(mod => result &= mod.PreHealthAdded(ref __instance, ref unrefamount));
        amount = unrefamount;

        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(ref PlayerHealth __instance, int amount)
    {
        Helper.PerformHook(mod => mod.PostHealthAdded(ref __instance, amount));
    }
}