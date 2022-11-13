namespace AvHModHelper.Patches.Enemy;

[HarmonyPatch(typeof(BananaScript), "OnTriggerEnter")]
internal static class BananaScript_OnTriggerEnter
{
    [HarmonyPrefix]
    internal static bool Prefix(ref Collider other)
    {
        var collider = other;

        var result = true;
        Helper.PerformHook(mod => result &= mod.PreBananaPickUp(ref collider));
        other = collider;

        return result;
    }

    [HarmonyPostfix]
    internal static void Postfix(ref Collider other)
    {
        var collider = other;

        Helper.PerformHook(mod => mod.PostBananaPickUp(ref collider));
        other = collider;
    }
}