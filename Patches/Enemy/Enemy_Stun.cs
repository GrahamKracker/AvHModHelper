namespace AvHModHelper.Patches.Enemy;

using Enemy = global::Enemy;

[HarmonyPatch(typeof(Enemy), nameof(Enemy.Stun))]
internal static class Enemy_Stun
{
    [HarmonyPrefix]
    internal static bool Prefix(ref Enemy __instance)
    {
        result = true;
        Helper.PerformHook(mod => result &= mod.PreBloonStunned(ref __instance));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(ref Enemy __instance)
    {
        Helper.PerformHook(mod => mod.PostBloonStunned(ref __instance));
    }
}