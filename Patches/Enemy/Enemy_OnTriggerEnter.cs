namespace AvHModHelper.Patches.Enemy;

[HarmonyPatch(typeof(global::Enemy), "OnTriggerEnter")]
internal static class Enemy_OnTriggerEnter
{
    [HarmonyPrefix]
    internal static bool Prefix(ref global::Enemy __instance, ref Collider other)
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
    internal static void Postfix(global::Enemy __instance, Collider other)
    {
        Helper.PerformHook(mod => mod.PostBloonCollides(__instance, other));
    }
}
