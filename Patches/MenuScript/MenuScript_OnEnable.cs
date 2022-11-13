namespace AvHModHelper.Patches.MenuScript;

using MenuScript = global::MenuScript;

[HarmonyPatch(typeof(MenuScript), "OnEnable")]
internal static class MenuScript_OnEnable
{
    [HarmonyPostfix]
    internal static void Postfix(MenuScript __instance)
    {
        Helper.PerformHook(mod => mod.OnMainMenu(__instance));
    }
}
