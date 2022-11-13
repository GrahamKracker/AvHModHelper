namespace AvHModHelper.Patches.EquipmentScript;

using EquipmentScript = global::EquipmentScript;

[HarmonyPatch(typeof(EquipmentScript), nameof(EquipmentScript.ChangeWeapon))]
internal static class EquipmentScript_ChangeWeapon
{
    [HarmonyPrefix]
    internal static bool Prefix(EquipmentScript __instance, ref string weaponID)
    {
        var result = true;
        var unrefweaponid = weaponID;
        Helper.PerformHook(mod => result &= mod.PreWeaponSwap(__instance, ref unrefweaponid));
        weaponID = unrefweaponid;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(EquipmentScript __instance, string weaponID)
    {
        Helper.PerformHook(mod => mod.PostWeaponSwap(__instance, weaponID));
    }
}