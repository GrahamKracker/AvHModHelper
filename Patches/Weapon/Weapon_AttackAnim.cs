namespace AvHModHelper.Patches.Weapon;

using Weapon = global::Weapon;

[HarmonyPatch(typeof(Weapon), "AttackAnim")]
internal static class Weapon_AttackAnim
{
    [HarmonyPrefix]
    internal static bool Prefix(ref Weapon __instance)
    {
        var result = true;
        var unref = __instance;
        Helper.PerformHook(mod => result &= mod.PreAttackAnim(ref unref));
        __instance = unref;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(Weapon __instance)
    {
        Helper.PerformHook(mod => mod.PostAttackAnim(__instance));
    }
}
