namespace AvHModHelper.Patches.FirstPersonController;

using UnityStandardAssets.Characters.FirstPerson;

[HarmonyPatch(typeof(FirstPersonController), "GetInput")]
internal static class FirstPersonController_GetInput
{
    [HarmonyPrefix]
    internal static bool Prefix(ref FirstPersonController __instance, ref float speed)
    {
        var player = __instance;
        var rate = speed;
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreInputReceived(ref player, ref rate));
        __instance = player;
        speed = rate;
        return result;
    }
    [HarmonyPostfix]
    internal static void Postfix(FirstPersonController __instance, float speed)
    {
        Helper.PerformHook(mod => mod.PostInputReceived(__instance, speed));
    }
}
