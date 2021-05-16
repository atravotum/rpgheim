using BepInEx;
using UnityEngine;
using HarmonyLib;
using System.Collections.Generic;
using BepInEx.Logging;
using RPGHeim.Managers;
using System.Collections;
using System.Linq;
using System;

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
            AssetManager.RegisterPrefabs();
            AssetManager.RegisterLocalization();
            SkillsManager.RegisterSkills();
            SEManager.RegisterStatusEffects();
            AbilitiesManager.RegisterAbilities();

            UIAbilityWindowManager = new UIAbilityWindowManager();
            UIHotBarManager = new UIHotBarManager();

            // run the harmony patches
            harmony.PatchAll();
        }

        private void Update()
        {
            InputManager.Update();
            UIHotBarManager.TickCooldowns();
            //if (RPGHeimHudSystem.isEnabled) RPGHeimHudSystem.SkillsBar.TickCooldowns();
        }
    }
}