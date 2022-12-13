namespace AvHModHelper.Patches.MenuScript;

[HarmonyPatch(typeof(global::MenuScript), "OnEnable")]
internal static class MenuScript_OnEnable
{
    [HarmonyPostfix]
    internal static void Postfix(global::MenuScript __instance)
    {
        Helper.PerformHook(mod => mod.OnMainMenuScript(__instance));
    }
}
