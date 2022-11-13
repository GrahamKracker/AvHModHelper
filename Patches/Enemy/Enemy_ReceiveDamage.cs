namespace AvHModHelper.Patches.Enemy;

using Enemy = global::Enemy;

[HarmonyPatch(typeof(Enemy), nameof(Enemy.ReceiveDamage))]
internal static class Enemy_ReceiveDamage
{
    [HarmonyPrefix]
    internal static bool Prefix(ref Enemy __instance, ref int dmg, ref string type, ref Projectile proj, ref bool damageOnSpawn, ref bool regrowthBlock)
    {
        var result = true;
        Helper.PerformHook(mod => result &= mod.PreBloonDamaged(ref __instance, ref dmg, ref type, ref proj, ref damageOnSpawn, ref regrowthBlock));
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(ref Enemy __instance, ref int dmg, ref string type, ref Projectile proj, ref bool damageOnSpawn, ref bool regrowthBlock)
    {
        Helper.PerformHook(mod => mod.PostBloonDamaged(ref __instance, ref dmg, ref type, ref proj, ref damageOnSpawn, ref regrowthBlock));
    }
}