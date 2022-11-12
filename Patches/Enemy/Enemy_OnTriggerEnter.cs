namespace AvHModHelper.Patches.Enemy;

using Enemy = global::Enemy;

[HarmonyPatch(typeof(Enemy), "OnTriggerEnter")]
internal class EnemyOnTriggerEnter_Patch
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
    internal static void Postfix(ref Enemy __instance, ref Collider other)
    {
        var enemy = __instance;
        var collider = other;
        Helper.PerformHook(mod => mod.PostBloonCollides(ref enemy, ref collider));
        __instance = enemy;
        other = collider;
    }
}
