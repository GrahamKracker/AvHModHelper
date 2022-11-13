namespace AvHModHelper.Patches.Enemy;

[HarmonyPatch(typeof(GameManagerScript), "Start")]
internal static class GameManagerScript_Start
{
    [HarmonyPrefix]
    internal static bool Prefix(GameManagerScript __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreGameManagerInit(__instance));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(GameManagerScript __instance)
    {
        Helper.PerformHook(mod => mod.PostGameManagerInit(__instance));
    }
}
