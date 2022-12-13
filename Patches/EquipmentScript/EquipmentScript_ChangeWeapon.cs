namespace AvHModHelper.Patches.EquipmentScript;

[HarmonyPatch(typeof(global::EquipmentScript), nameof(global::EquipmentScript.ChangeWeapon))]
internal static class EquipmentScript_ChangeWeapon
{
    [HarmonyPrefix]
    internal static bool Prefix(ref global::EquipmentScript __instance, ref string weaponID)
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
    internal static void Postfix(global::EquipmentScript __instance, string weaponID)
    {
        Helper.PerformHook(mod => mod.PostWeaponSwap(__instance, weaponID));
    }
}
