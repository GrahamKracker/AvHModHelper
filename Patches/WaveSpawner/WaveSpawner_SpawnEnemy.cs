namespace AvHModHelper.Patches.WaveSpawner;

[HarmonyPatch(typeof(global::WaveSpawner), "SpawnEnemy")]
internal static class WaveSpawner_SpawnEnemy
{
    [HarmonyPostfix]
    internal static void Postfix(global::WaveSpawner __instance, Transform _enemy)
    {
        Helper.PerformHook(mod => mod.OnEnemySpawned(__instance, _enemy));
    }
}