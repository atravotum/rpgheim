using BepInEx;
using UnityEngine;
using HarmonyLib;
using RPGHeim.Managers;

namespace RPGHeim
{
    [BepInPlugin("github.atravotum.rpgheim", "RPGHeim", "1.0.0")]
    [BepInDependency(Jotunn.Main.ModGuid)]

    internal class RPGHeimMain : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("github.atravotum.rpgheim");

        // -- Random Note:
        // Add Unlock - Overlay window : Text Description + Button.
        // So we can have unlockable skills. (Could be at a cost, or just level)
        // Could even make it require them to be within a specific trigger collider (simple area check)
        // Maybe you could only unlock abilitys near the "Table" or "Trainer"
        public static UIAbilityWindowManager UIAbilityWindowManager { get; set; }
        public static UIHotBarManager UIHotBarManager { get; set; }

        private void Awake()
        {
            // load in all teh required assets for the mod
            SkillsManager.RegisterSkills();

            AssetManager.RegisterPrefabs();
            AssetManager.RegisterLocalization();
            SEManager.RegisterStatusEffects();
            AbilitiesManager.RegisterAbilities();

            UIAbilityWindowManager = new UIAbilityWindowManager();

            // run the harmony patches
            harmony.PatchAll();
        }

        private void Update()
        {
            InputManager.Update();
            if (UIHotBarManager != null)
            {
                UIHotBarManager.TickCooldowns();
            }
            //if (RPGHeimHudSystem.isEnabled) RPGHeimHudSystem.SkillsBar.TickCooldowns();
        }
    }
}