namespace AvHModHelper.Patches.Enemy;

using Enemy = global::Enemy;

[HarmonyPatch(typeof(Enemy), nameof(Enemy.Start))]
internal static class Enemy_Start
{
    [HarmonyPrefix]
    internal static bool Prefix(ref Enemy __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreBloonLoaded(ref __instance));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(ref Enemy __instance)
    {
        Helper.PerformHook(mod => mod.PostBloonLoaded(ref __instance));
    }
}