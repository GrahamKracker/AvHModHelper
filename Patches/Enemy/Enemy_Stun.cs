namespace AvHModHelper.Patches.Enemy;

using Enemy = global::Enemy;

[HarmonyPatch(typeof(Enemy), nameof(Enemy.Stun))]
internal static class Enemy_Stun
{
    [HarmonyPrefix]
    internal static bool Prefix(Enemy __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreBloonStunned(__instance));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(Enemy __instance)
    {
        Helper.PerformHook(mod => mod.PostBloonStunned(__instance));
    }
}