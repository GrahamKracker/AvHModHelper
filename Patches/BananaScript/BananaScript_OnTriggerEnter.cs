namespace AvHModHelper.Patches.BananaScript;

using BananaScript = global::BananaScript;

[HarmonyPatch(typeof(BananaScript), "OnTriggerEnter")]
internal static class BananaScript_OnTriggerEnter
{
    [HarmonyPrefix]
    internal static bool Prefix(ref Collider other)
    {
        var collider = other;
        var result = true;
        if (other.tag == "Player")
        {
            Helper.PerformHook(mod => result &= mod.PreBananaPickUp(ref collider));
        }
        other = collider;

        return result;

    }

    [HarmonyPostfix]
    internal static void Postfix(Collider other)
    {
        if (other.tag == "Player")
        {
            Helper.PerformHook(mod => mod.PostBananaPickUp(ref other));
        }
    }
}