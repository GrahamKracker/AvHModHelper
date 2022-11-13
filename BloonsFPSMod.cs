namespace AvHModHelper;

using System;
using System.Collections.Generic;
using Extensions;

public abstract class AvHMod : MelonMod
{
    internal List<string> loadErrors = new();

    /// <summary>
    ///     Lets the ModHelper control patching, allowing for individual patches to fail without the entire mod getting
    ///     unloaded.
    /// </summary>
    internal bool modHelperPatchAll;

    /// <summary>
    ///     The embedded resources of this mod
    /// </summary>
    public Dictionary<string, byte[]> Resources { get; internal set; }

    /// <summary>
    ///     The prefix used for IDs to prevent conflicts with other mods
    /// </summary>
    public virtual string IDPrefix => this.GetAssembly().GetName().Name + "-";

    /// <summary>
    ///     Signifies that the game shouldn't crash / the mod shouldn't stop loading if one of its patches fails
    /// </summary>
    public virtual bool OptionalPatches => true;

    /// <inheritdoc />
    public sealed override void OnEarlyInitializeMelon()
    {
        // If they haven't set OptionalPatches to false and haven't already signified they have their own patching plan
        // by using HarmonyDontPatchAll themselves...
        if (OptionalPatches && !MelonAssembly.HarmonyDontPatchAll)
        {
            typeof(MelonAssembly).GetProperty(nameof(MelonAssembly.HarmonyDontPatchAll))!.GetSetMethod(true)!.Invoke(MelonAssembly, new object[] {true});
            modHelperPatchAll = true;
        }

        OnEarlyInitialize();
    }

    /// <inheritdoc />
    public sealed override void OnInitializeMelon()
    {
        if (modHelperPatchAll)
            AccessTools.GetTypesFromAssembly(this.GetAssembly()).Do(type =>
            {
                try
                {
                    HarmonyInstance.CreateClassProcessor(type).Patch();
                }
                catch (Exception e)
                {
                    MelonLogger.Error($"Failed to apply {Info.Name} patch(es) in {type.Name}: \"{e.Message}\" " + $"The mod might not function correctly. This needs to be fixed by {Info.Author}");
                    loadErrors.Add($"Failed to apply patch(es) in {type.Name}");
                }
            });
        OnApplicationStart();
        OnInitialize();
    }

    /// <inheritdoc cref="OnInitializeMelon" />
    public new virtual void OnApplicationStart()
    {
    }

    public override void OnSceneWasInitialized(int buildIndex, string sceneName)
    {
        if (sceneName != "MainMenu")
            OnMapLoaded(sceneName);
        else if (sceneName == "MainMenu") OnMainMenuLoaded();
    }

    /// <summary>
    ///     When scene is called, can't be the main menu.
    ///     <br />
    ///     Equivalent to OnSceneWasInitialized, but only called when the scene is not the main menu
    /// </summary>
    public virtual void OnMapLoaded(string mapName)
    {
    }

    /// <summary>
    ///     Called after player being in collision range of bloons / getting hit.
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on Enemy.OnTriggerEnter
    /// </summary>
    public virtual void PostBloonCollides(ref Enemy bloon, ref Collider other)
    {
    }

    /// <summary>
    ///     Called before player being in collision range of bloons / player not being hit yet.
    ///     <br />
    ///     Equivalent to a HarmonyPreFix on Enemy.OnTriggerEnter
    /// </summary>
    public virtual bool PreBloonCollides(ref Enemy bloon, ref Collider other)
    {
        return true;
    }
    /// <summary>
    ///     Called at the same time as the main menu is loaded.
    ///     <br />
    ///     Equivalent to OnSceneWasInitialized, if scene is the main menu.
    /// </summary>
    public virtual void OnMainMenuLoaded()
    {
    }

    /// <summary>
    ///     Called after a bloon is stunned
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on Enemy.Stun
    /// </summary>
    public virtual void PostBloonStunned(Enemy bloon)
    {
    }

    /// <summary>
    ///     Called before a bloon is stunned
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on Enemy.Stun
    /// </summary>
    public virtual bool PreBloonStunned(Enemy bloon)
    {
        return true;
    }

    /// <summary>
    ///     Called after cash is added to the player's balance
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on Currency.UpdateCurrency
    /// </summary>
    public virtual void PostCashAdded(Currency currency, int amount, bool doubled)
    {
    }

    /// <summary>
    ///     Called before cash is added to the player's balance, return false to prevent the cash from being added
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on Currency.UpdateCurrency
    /// </summary>
    public virtual bool PreCashAdded(Currency currency, ref int amount, ref bool canDouble)
    {
        return true;
    }

    /// <summary>
    ///     Called before a bloon is damaged, return false to prevent the damage
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on Enemy.RecieveDamage
    /// </summary>
    public virtual bool PreBloonDamaged(Enemy bloon, int dmg, string type, Projectile proj, bool damageOnSpawn, bool regrowthBlock)
    {
        return true;
    }

    /// <summary>
    ///     Called after a bloon is damaged
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on Enemy.RecieveDamage
    /// </summary>
    public virtual void PostBloonDamaged(Enemy bloon, int dmg, string type, Projectile proj, bool damageOnSpawn, bool regrowthBlock)
    {
    }

    /// <summary>
    ///     Called after a bloon is created.
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on Enemy.OnCreate
    /// </summary>
    public virtual void PostBloonLoaded(Enemy bloon)
    {
    }

    /// <summary>
    ///     Called before a bloon is created.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on Enemy.OnCreate
    /// </summary>
    public virtual bool PreBloonLoaded(ref Enemy bloon)
    {
        return true;
    }

    /// <summary>
    ///     Called before the game starts and the player is given a balance.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on Currency.Start
    /// </summary>
    public virtual bool PreCashLoaded(Currency currency)
    {
        return true;
    }

    /// <summary>
    ///     Called after the game starts and the player is given a balance.
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on Currency.Start
    /// </summary>
    public virtual void PostCashLoaded(Currency currency)
    {
    }

    /// <summary>
    ///     Called before a banana is picked up.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on BananaScript.OnTriggerEnter
    /// </summary>
    public virtual bool PreBananaPickUp(ref Collider other)
    {
        return true;
    }

    /// <summary>
    ///     Called after a banana is picked up.
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on BananaScript.OnTriggerEnter
    /// </summary>
    public virtual void PostBananaPickUp(ref Collider other)
    {
    }

    /// <summary>
    ///     Called before the player is removed health.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on PlayerHealth.UpdateHealth
    /// </summary>
    public virtual bool PreHealthAdded(PlayerHealth health, ref int amount)
    {
        return true;
    }

    /// <summary>
    ///     Called after the player is removed health.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on PlayerHealth.UpdateHealth
    /// </summary>
    public virtual void PostHealthAdded(PlayerHealth health, int amount)
    {
    }

    /// <summary>
    ///     Called before the game manager is initialized
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on GameManagerScript.Start
    /// </summary>
    public virtual bool PreGameManagerInit(GameManagerScript manager)
    {
        return true;
    }

    /// <summary>
    ///     Called after the game manager is initialized
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on GameManagerScript.Start
    /// </summary>
    public virtual void PostGameManagerInit(GameManagerScript manager)
    {
    }

    /// <inheritdoc cref="OnEarlyInitializeMelon" />
    public virtual void OnEarlyInitialize()
    {
    }

    /// <inheritdoc cref="OnInitializeMelon" />
    public virtual void OnInitialize()
    {
    }

    #region WaveSpawner Hooks

    /// <summary>
    ///     Called when a wave ends
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on WaveSpawner.WaveCompleted
    /// </summary>
    public virtual void OnWaveCompleted(WaveSpawner spawner)
    {
    }

    /// <summary>
    ///     Called after a wave spawner is initialized
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on WaveSpawner.Start
    /// </summary>
    public virtual void PostWaveSpawnerInit(WaveSpawner spawner)
    {
    }

    /// <summary>
    ///     Called before a wave spawner is initialized
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on WaveSpawner.Start
    /// </summary>
    public virtual bool PreWaveSpawnerInit(WaveSpawner spawner)
    {
        return true;
    }

    /// <summary>
    ///     Called when a bloon is spawned
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on WaveSpawner.SpawnEnemy
    /// </summary>
    public virtual void OnEnemySpawned(WaveSpawner spawner, Transform enemy)
    {
    }

    /// <summary>
    ///     Called when a wave starts
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on WaveSpawner.SpawnWave
    /// </summary>
    public virtual void OnWaveStarted(WaveSpawner spawner, WaveSpawner.Wave wave)
    {
    }

    /// <summary>
    ///     Called before every update
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on WaveSpawner.Update
    /// </summary>
    public virtual bool PreWaveUpdate(WaveSpawner spawner)
    {
        return true;
    }

    /// <summary>
    ///     Called after every update
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on WaveSpawner.Update
    /// </summary>
    public virtual void PostWaveUpdate(WaveSpawner spawner)
    {
    }

    #endregion
}
