namespace AvHModHelper.Patches.Enemy;

using Enemy = global::Enemy;

[HarmonyPatch(typeof(Enemy), nameof(Enemy.Start))]
internal static class Enemy_Start
{
    [HarmonyPrefix]
    internal static bool Prefix(ref Enemy __instance)
    {
        var result = true;
        var unref = __instance;
        Helper.PerformHook(mod => result &= mod.PreBloonLoaded(ref unref));
        __instance = unref;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(Enemy __instance)
    {
        Helper.PerformHook(mod => mod.PostBloonLoaded(__instance));
    }
}
