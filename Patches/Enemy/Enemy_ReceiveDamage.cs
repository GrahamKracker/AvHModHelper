namespace AvHModHelper.Patches.Enemy;

using Enemy = global::Enemy;

[HarmonyPatch(typeof(Enemy), nameof(Enemy.ReceiveDamage))]
internal static class Enemy_ReceiveDamage
{
    [HarmonyPrefix]
    internal static bool Prefix(Enemy __instance, int dmg, string type, Projectile proj, bool damageOnSpawn, bool regrowthBlock)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreBloonDamaged(__instance, dmg, type, proj, damageOnSpawn, regrowthBlock));
        return result;
    }
    [HarmonyPostfix]
    internal static void Postfix(Enemy __instance, int dmg, string type, Projectile proj, bool damageOnSpawn, bool regrowthBlock)
    {
        Helper.PerformHook(mod => mod.PostBloonDamaged(__instance, dmg, type, proj, damageOnSpawn, regrowthBlock));
    }
}
