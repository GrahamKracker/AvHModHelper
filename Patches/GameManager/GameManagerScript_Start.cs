namespace AvHModHelper.Patches.GameManagerScript;

using GameManagerScript = global::GameManagerScript;

[HarmonyPatch(typeof(GameManagerScript), "Start")]
internal static class GameManagerScript_Start
{
    [HarmonyPrefix]
    internal static bool Prefix(ref GameManagerScript __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreGameManagerInit(ref __instance));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(ref GameManagerScript __instance)
    {
        Helper.PerformHook(mod => mod.PostGameManagerInit(ref __instance));
    }
}
