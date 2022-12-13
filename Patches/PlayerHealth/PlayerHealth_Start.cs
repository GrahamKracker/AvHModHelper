namespace AvHModHelper.Patches.PlayerHealth;

[HarmonyPatch(typeof(global::PlayerHealth), "Start")]
internal static class PlayerHealth_Start
{
    [HarmonyPrefix]
    internal static bool Prefix(global::PlayerHealth __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreHealthLoaded(__instance));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(global::PlayerHealth __instance)
    {
        Helper.PerformHook(mod => mod.PostHealthLoaded(__instance));
    }
}
