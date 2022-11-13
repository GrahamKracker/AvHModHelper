namespace AvHModHelper.Patches.PlayerHealth;

using PlayerHealth = global::PlayerHealth;

[HarmonyPatch(typeof(PlayerHealth), "Start")]
internal static class PlayerHealth_Start
{
    [HarmonyPrefix]
    internal static bool Prefix(PlayerHealth __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreHealthLoaded(__instance));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(PlayerHealth __instance)
    {
        Helper.PerformHook(mod => mod.PostHealthLoaded(__instance));
    }
}
