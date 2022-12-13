namespace AvHModHelper.Patches.GameManagerScript;

[HarmonyPatch(typeof(global::GameManagerScript), "Start")]
internal static class GameManagerScript_Start
{
    [HarmonyPrefix]
    internal static bool Prefix(ref global::GameManagerScript __instance)
    {
        var result = true;
        var unref = __instance;
        Helper.PerformHook(mod => result &= mod.PreGameManagerInit(ref unref));
        __instance = unref;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(global::GameManagerScript __instance)
    {
        Helper.PerformHook(mod => mod.PostGameManagerInit(__instance));
    }
}
