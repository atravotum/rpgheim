using BepInEx;
using Jotunn.Configs;
using Jotunn.Managers;
using UnityEngine;
using HarmonyLib;

namespace RPGHeim
{
    [BepInPlugin("github.atravotum.rpgheim", "RPGHeim", "1.0.0")]
    [BepInDependency(Jotunn.Main.ModGuid)]

    internal class RPGHeimMain : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("github.atravotum.rpgheim");

        private void Awake()
        {
            AssetManager.RegisterPrefabs();
            AssetManager.RegisterLocalization();
            AddLocalizations();
            harmony.PatchAll();
        }

        /// <summary>
        /// Game tick updates - Check for custom inputs
        /// </summary>
        private void Update()
        {
            // Since our Update function in our BepInEx mod class will load BEFORE Valheim loads,
            // we need to check that ZInput is ready to use first.
            if (ZInput.instance != null)
            {
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha1))
                {
                    AssetManager.ProjectileIndex++;
                    if (AssetManager.ProjectileIndex >= AssetManager.ProjectilesPrefabs.Count)
                    {
                        AssetManager.ProjectileIndex = 0;
                    }
                }
            }
        }

        // Adds localizations with configs
        private void AddLocalizations()
        {
            // Add static translations *todo later change this to be imported
            LocalizationManager.Instance.AddLocalization(new LocalizationConfig("English")
            {
                Translations = {
                    {"piece_RPGHeimClassStone", "Class Stone"}, {"piece_RPGHeimClassStone_description", "Gain access to RPGHeim's class items/gameplay."},
                    {"item_RPGHeimTomeFighter", "Fighter Class Tome"}, {"item_RPGHeimTomeFighter_description", "Unlock your true potential as a skilled figher."},
                }
            });
        }
    }
}