namespace AvHModHelper.Patches.FirstPersonController;

using FirstPersonController = UnityStandardAssets.Characters.FirstPerson.FirstPersonController;

[HarmonyPatch(typeof(FirstPersonController), "GetInput")]
internal static class EquipmentScript_ChangeWeapon
{
    [HarmonyPrefix]
    internal static bool Prefix(FirstPersonController __instance, ref float speed)
    {
        var result = true;
        var unrefspeed = speed;
        Helper.PerformHook(mod => result &= mod.PrePlayerMovement(__instance, ref unrefspeed));
        speed = unrefspeed;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(FirstPersonController __instance, float speed)
    {
        Helper.PerformHook(mod => mod.PostPlayerMovement(__instance, speed));
    }
}