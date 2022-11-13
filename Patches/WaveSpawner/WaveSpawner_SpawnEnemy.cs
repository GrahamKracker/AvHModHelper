namespace AvHModHelper.Patches.WaveSpawner;

using WaveSpawner = global::WaveSpawner;

[HarmonyPatch(typeof(WaveSpawner), "SpawnEnemy")]
internal static class WaveSpawner_SpawnEnemy
{
    [HarmonyPostfix]
    internal static void Postfix(ref WaveSpawner __instance, ref Transform _enemy)
    {
        Helper.PerformHook(mod => mod.OnEnemySpawned(ref __instance, ref _enemy));
    }
}