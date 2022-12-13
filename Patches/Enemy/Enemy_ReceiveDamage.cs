namespace AvHModHelper.Patches.Enemy;

[HarmonyPatch(typeof(global::Enemy), nameof(global::Enemy.ReceiveDamage))]
internal static class Enemy_ReceiveDamage
{
    [HarmonyPrefix]
    internal static bool Prefix(ref global::Enemy __instance, ref int dmg, ref string type, ref Projectile proj, ref bool regrowthBlock)
    {
        var result = true;
        var unref = __instance;
        var unref2 = dmg;
        var unref3 = type;
        var unref4 = proj;
        var unref6 = regrowthBlock;
        Helper.PerformHook(mod => result &= mod.PreBloonDamaged(ref unref, ref unref2, ref unref3, ref unref4, ref unref6));
        __instance = unref;
        dmg = unref2;
        type = unref3;
        proj = unref4;
        regrowthBlock = unref6;
        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(global::Enemy __instance,int dmg, string type, Projectile proj, bool regrowthBlock)
    {
        Helper.PerformHook(mod => mod.PostBloonDamaged(__instance, dmg, type, proj, regrowthBlock));
    }
}
