using MelonLoader;


namespace AvHModHelper
{
    using System;
    using System.Collections.Generic;
    using Extensions;
    using HarmonyLib;

    public abstract class AvHMod : MelonMod
    {
        /// <summary>
        /// The embedded resources of this mod
        /// </summary>
        public Dictionary<string, byte[]> Resources { get; internal set; }
        
        /// <summary>
        /// The prefix used for IDs to prevent conflicts with other mods
        /// </summary>
        public virtual string IDPrefix => this.GetAssembly().GetName().Name + "-";

        internal List<string> loadErrors = new();

        /// <summary>
        /// Signifies that the game shouldn't crash / the mod shouldn't stop loading if one of its patches fails
        /// </summary>
        public virtual bool OptionalPatches => true;
        
        /// <summary>
        /// Lets the ModHelper control patching, allowing for individual patches to fail without the entire mod getting
        /// unloaded. 
        /// </summary>
        internal bool modHelperPatchAll;

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
            {
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
            }
            OnApplicationStart();
            OnInitialize();
        }

        /// <inheritdoc cref="OnInitializeMelon"/>
        public new virtual void OnApplicationStart()
        {
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName != "MainMenu")
            {
                OnMapLoaded(sceneName);
            }
            else if (sceneName == "MainMenu")
            {
                OnMainMenuLoaded();
            }
        }

        /// <summary>
        /// Called when a map is loaded
        /// <br/>
        /// Equivalent to OnSceneWasInitialized, but only called when the scene is not the main menu
        /// </summary>
        public virtual void OnMapLoaded(string mapName)
        {
        }

        /// <summary>
        /// Called when the mainmenu is loaded
        /// <br/>
        /// Equivalent to OnSceneWasInitialized, but only called when the scene is the main menu
        /// </summary>
        public virtual void OnMainMenuLoaded()
        {
        }
        /// <summary>
        /// Called after a bloon is stunned
        /// <br/>
        /// Equivalent to a HarmonyPostFix on Enemy.Start
        /// </summary>
        public virtual void OnBloonStunned(Enemy bloon)
        {
        }

        /// <summary>
        /// Called before cash is added to the player's balance, return false to prevent the cash from being added
        /// <br/>
        /// Equivalent to a HarmonyPostFix on Currency.UpdateCurrency
        /// </summary>
        public virtual void PostCashAdded(Currency __instance, int amount, bool doubled)
        {
        }
        
        /// <summary>
        /// Called before cash is added to the player's balance, return false to prevent the cash from being added
        /// <br/>
        /// Equivalent to a HarmonyPostFix on Currency.UpdateCurrency
        /// </summary>
        public virtual bool PreCashAdded(Currency __instance, ref int amount, ref bool canDouble)
        {
            return true;
        }
        
        /// <summary>
        /// Called before a bloon is damaged, return false to prevent the damage
        /// <br/>
        /// Equivalent to a HarmonyPostFix on Enemy.Start
        /// </summary>
        public virtual bool PreBloonDamaged(Enemy bloon, int dmg, string type, Projectile proj, bool damageOnSpawn, bool regrowthBlock)
        {
            return true;
        }
        /// <summary>
        /// Called after a bloon is damaged
        /// <br/>
        /// Equivalent to a HarmonyPostFix on Enemy.Start
        /// </summary>
        public virtual void PostBloonDamaged(Enemy bloon, int dmg, string type, Projectile proj, bool damageOnSpawn, bool regrowthBlock)
        {
        }

        /// <summary>
        /// Called after a bloon's properties are first loaded into the game
        /// <br/>
        /// Equivalent to a HarmonyPostFix on Enemy.Start
        /// </summary>
        public virtual void PostBloonLoaded(Enemy bloon)
        {
        }
        
        /// <summary>
        /// Called before a bloon's properties are first loaded into the game. Set to false to prevent the bloon from loading
        /// <br/>
        /// Equivalent to a HarmonyPostFix on Enemy.Start
        /// </summary>
        public virtual bool PreBloonLoaded(ref Enemy bloon)
        {
            return true;
        }
        
        #region WaveSpawner Hooks

        /// <summary>
        /// Called when a wave ends
        /// <br/>
        /// Equivalent to a HarmonyPostFix on WaveSpawner.WaveCompleted
        /// </summary>
        public virtual void OnWaveCompleted(WaveSpawner spawner)
        {
        }
        /// <summary>
        /// Called after a wavespawner is initialized
        /// <br/>
        /// Equivalent to a HarmonyPostFix on WaveSpawner.Start
        /// </summary>
        public virtual void OnWaveSpawnerInit(WaveSpawner spawner)
        {
        }

        /// <summary>
        /// Called when a bloon is spawned
        /// <br/>
        /// Equivalent to a HarmonyPostFix on WaveSpawner.SpawnEnemy
        /// </summary>
        public virtual void OnEnemySpawned(WaveSpawner spawner, Transform enemy)
        {
        }
        /// <summary>
        /// Called when a wave starts
        /// <br/>
        /// Equivalent to a HarmonyPostFix on WaveSpawner.SpawnWave
        /// </summary>
        public virtual void OnWaveStarted(WaveSpawner spawner, WaveSpawner.Wave wave)
        {
        }
        
        #endregion

        /// <inheritdoc cref="OnEarlyInitializeMelon"/>
        public virtual void OnEarlyInitialize()
        {
        }

        /// <inheritdoc cref="OnInitializeMelon"/>
        public virtual void OnInitialize()
        {
        }
    }
}
