namespace AvHModHelper;

using System;
using System.Collections.Generic;
using Extensions;
using UnityStandardAssets.Characters.FirstPerson;

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
        else if (sceneName == "MainMenu") OnMainMenuScene();
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
    ///     Called after the MenuScript is loaded
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on MenuScript.OnEnable
    /// </summary>
    public virtual void OnMainMenuScript(MenuScript mainMenu)
    {
    }
    /// <summary>
    ///     Called when a map is loaded
    ///     <br />
    ///     Equivalent to OnSceneWasInitialized, but only called when the scene is not the main menu
    /// </summary>
    public virtual void PostInputReceived(FirstPersonController player, float speed)
    {
    }
    
    /// <summary>
    ///     Called before the player receives input, return false to stop the original method from running.
    ///     <br />
    ///     Equivalent to OnSceneWasInitialized, but only called when the scene is not the main menu
    /// </summary>
    public virtual bool PreInputReceived(ref FirstPersonController player, ref float speed)
    {
        return true;
    }
    /// <summary>
    ///     Called after player being in collision range of bloons / getting hit.
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on Enemy.OnTriggerEnter
    /// </summary>
    public virtual void PostBloonCollides(Enemy bloon, Collider other)
    {
    }

    /// <summary>
    ///     Called before player being in collision range of bloons / player not being hit yet, return false to stop the original method from running.
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
    public virtual void OnMainMenuScene()
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
    ///     Called before a bloon is stunned, return false to stop the original method from running.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on Enemy.Stun
    /// </summary>
    public virtual bool PreBloonStunned(ref Enemy bloon)
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
    ///     Called before cash is added to the player's balance, return false to stop the original method from running
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on Currency.UpdateCurrency
    /// </summary>
    public virtual bool PreCashAdded(ref Currency currency, ref int amount, ref bool canDouble)
    {
        return true;
    }

    /// <summary>
    ///     Called before a bloon is damaged, return false to stop the original method from running
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on Enemy.RecieveDamage
    /// </summary>
    public virtual bool PreBloonDamaged(ref Enemy bloon, ref int dmg, ref string type, ref Projectile proj, ref bool damageOnSpawn, ref bool regrowthBlock)
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
    ///     Called after a bloon is given properties.
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on Enemy.OnCreate
    /// </summary>
    public virtual void PostBloonLoaded(Enemy bloon)
    {
    }

    /// <summary>
    ///     Called before a bloon is given properties, return false to stop the original method from running.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on Enemy.OnCreate
    /// </summary>
    public virtual bool PreBloonLoaded(ref Enemy bloon)
    {
        return true;
    }

    /// <summary>
    ///     Called before the game starts and the player is given a balance, return false to stop the original method from running.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on Currency.Start
    /// </summary>
    public virtual bool PreCashLoaded(ref Currency currency)
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
    ///     Called before a banana is picked up, return false to stop the original method from running.
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
    public virtual void PostBananaPickUp(Collider collider)
    {
    }

    /// <summary>
    ///     Called before the player is removed health, return false to stop the original method from running.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on PlayerHealth.UpdateHealth
    /// </summary>
    public virtual bool PreHealthAdded(ref PlayerHealth health, ref int amount)
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
    ///     Called before the game manager is initialized, return false to stop the original method from running.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on GameManagerScript.Start
    /// </summary>
    public virtual bool PreGameManagerInit(ref GameManagerScript manager)
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

    /// <summary>
    ///     Called before the banana decays, return false to stop the original method from running.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on BananaScript.Decay
    /// </summary>
    public virtual bool PreBananaDecay(ref BananaScript banana)
    {
        return true;
    }

    /// <summary>
    ///     Called after the banana has decayed
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on GameManagerScript.Start
    /// </summary>
    public virtual void PostBananaDecay(BananaScript banana)
    {
    }

    /// <summary>
    ///     Called after a bloon has spawned
    ///     <br />
    ///     Equivalent to a HarmonyPostfix on Enemy.Start
    /// </summary>
    public virtual void PostBloonSpawn(Enemy bloon)
    {
    }

    /// <summary>
    ///     Called before a bloon has spawned, return false to stop the original method from running.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on Enemy.Start
    /// </summary>
    public virtual bool PreBloonSpawn(ref Enemy bloon)
    {
        return true;
    }

    /// <summary>
    ///     Called after the player is given health.
    ///     <br />
    ///     Equivalent to a HarmonyPostfix on Enemy.Start
    /// </summary>
    public virtual void PostHealthLoaded(PlayerHealth health)
    {
    }

    /// <summary>
    ///     Called before the player is given health, return false to stop the original method from running.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on Enemy.Start
    /// </summary>
    public virtual bool PreHealthLoaded(PlayerHealth health)
    {
        return true;
    }

    /// <summary>
    ///     Called after player sells weapons.
    ///     <br />
    ///     Equivalent to a HarmonyPostfix on Currency.CashBack
    /// </summary>
    public virtual void PostSell(Currency currency)
    {
    }

    /// <summary>
    ///     Called before player sells weapons, return false to stop the original method from running.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on Currency.CashBack
    /// </summary>
    public virtual bool PreSell(ref Currency currency)
    {
        return true;
    }

    /// <summary>
    ///     Called after player attacks.
    ///     <br />
    ///     Equivalent to a HarmonyPostfix on Weapon.AttackAnim
    /// </summary>
    public virtual void PostAttackAnim(Weapon weapon)
    {
    }

    /// <summary>
    ///     Called before player attacks, return false to stop the original method from running.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on Weapon.AttackAnim
    /// </summary>
    public virtual bool PreAttackAnim(ref Weapon weapon)
    {
        return true;
    }

    /// <summary>
    ///     Called after the player's weapon gets switched.
    ///     <br />
    ///     Equivalent to a HarmonyPostfix on EquipmentScript.ChangeWeapon
    /// </summary>
    public virtual void PostWeaponSwap(EquipmentScript equipment, string weaponName)
    {
    }

    /// <summary>
    ///     Called before the player's weapon gets switched, return false to stop the original method from running.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on EquipmentScript.ChangeWeapon
    /// </summary>
    public virtual bool PreWeaponSwap(ref EquipmentScript equipment, ref string weaponName)
    {
        return true;
    }

    /// <summary>
    ///     Called after the player moves, return false to stop the original method from running.
    ///     <br />
    ///     Equivalent to a HarmonyPostfix on EquipmentScript.ChangeWeapon
    /// </summary>
    public virtual void PostPlayerMovement(FirstPersonController controller, float speed)
    {
    }

    /// <summary>
    ///     Called before the player moves, return false to stop the original method from running.
    ///     <br />
    ///     Equivalent to a HarmonyPrefix on EquipmentScript.ChangeWeapon
    /// </summary>
    public virtual bool PrePlayerMovement(ref FirstPersonController controller, ref float speed)
    {
        return true;
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
    ///     Called after a bloon is spawned
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
    ///     Called before every wavespawner update
    ///     <br />
    ///     Equivalent to a HarmonyPostFix on WaveSpawner.Update
    /// </summary>
    public virtual bool PreWaveUpdate(ref WaveSpawner spawner)
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
