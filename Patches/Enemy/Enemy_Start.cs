namespace AvHModHelper.Patches.Enemy;

using Enemy = global::Enemy;

[HarmonyPatch(typeof(Enemy), nameof(Enemy.Start))]
internal static class Enemy_Start
{
    [HarmonyPrefix]
    internal static bool Prefix(Enemy __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreBloonLoaded(ref __instance));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(Enemy __instance)
    {
        Helper.PerformHook(mod => mod.PostBloonLoaded(__instance));
    }
}