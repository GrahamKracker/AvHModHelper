namespace AvHModHelper.Patches.Weapon;

using Weapon = global::Weapon;

[HarmonyPatch(typeof(Weapon), "AttackAnim")]
internal static class Weapon_AttackAnim
{
    [HarmonyPrefix]
    internal static bool Prefix(Weapon __instance)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreAttackAnim(__instance));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(Weapon __instance)
    {
        Helper.PerformHook(mod => mod.PostAttackAnim(__instance));
    }
}
