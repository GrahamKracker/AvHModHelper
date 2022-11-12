namespace AvHModHelper.Patches.Enemy;

using Enemy = global::Enemy;

[HarmonyPatch(typeof(Enemy), nameof(Enemy.Stun))]
internal static class Enemy_Stun
{
    [HarmonyPostfix]
    internal static void Postfix(Enemy __instance)
    {
        Helper.PerformHook(mod => mod.OnBloonStunned(__instance));
    }
}