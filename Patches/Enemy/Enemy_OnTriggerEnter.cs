namespace AvHModHelper.Patches.Enemy;

using Enemy = global::Enemy;

[HarmonyPatch(typeof(Enemy), "OnTriggerEnter")]
internal static class Enemy_OnTriggerEnter
{
    [HarmonyPrefix]
    internal static bool Prefix(ref Enemy __instance, ref Collider other)
    {
        var enemy = __instance;
        var collider = other;
        
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreBloonCollides(ref enemy, ref collider));
        __instance = enemy;
        other = collider;
        
        return result;
    }
    [HarmonyPostfix]
    internal static void Postfix(Enemy __instance, Collider other)
    {
        Helper.PerformHook(mod => mod.PostBloonCollides(__instance, other));
    }
}
