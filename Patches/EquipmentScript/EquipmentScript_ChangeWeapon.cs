namespace AvHModHelper.Patches.EquipmentScript;

using EquipmentScript = global::EquipmentScript;

[HarmonyPatch(typeof(EquipmentScript), nameof(EquipmentScript.ChangeWeapon))]
internal static class EquipmentScript_ChangeWeapon
{
    [HarmonyPrefix]
    internal static bool Prefix(ref EquipmentScript __instance, ref string weaponID)
    {
        var result = true;
        var unrefweaponid = weaponID;
        var unref = __instance;
        Helper.PerformHook(mod => result &= mod.PreWeaponSwap(ref unref, ref unrefweaponid));
        __instance = unref;
        weaponID = unrefweaponid;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(EquipmentScript __instance, string weaponID)
    {
        Helper.PerformHook(mod => mod.PostWeaponSwap(__instance, weaponID));
    }
}
