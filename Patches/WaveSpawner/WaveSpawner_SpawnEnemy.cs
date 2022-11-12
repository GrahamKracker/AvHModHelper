namespace AvHModHelper.Patches.WaveSpawner;

using WaveSpawner = global::WaveSpawner;

[HarmonyPatch(typeof(WaveSpawner), "SpawnEnemy")]
internal static class WaveSpawner_SpawnEnemy
{
    [HarmonyPostfix]
    internal static void Postfix(WaveSpawner __instance, Transform _enemy)
    {
        Helper.PerformHook(mod => mod.OnEnemySpawned(__instance, _enemy));
    }
}
