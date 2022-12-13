namespace AvHModHelper.Patches.Enemy;

[HarmonyPatch(typeof(global::Enemy), nameof(global::Enemy.Stun))]
internal static class Enemy_Stun
{
    [HarmonyPrefix]
    internal static bool Prefix(ref global::Enemy __instance)
    {
        var result = true;
        var unref = __instance;
        Helper.PerformHook(mod => result &= mod.PreBloonStunned(ref unref));
        __instance = unref;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(global::Enemy __instance)
    {
        Helper.PerformHook(mod => mod.PostBloonStunned(__instance));
    }
}
