using BepInEx;
using UnityEngine;
using HarmonyLib;
using Jotunn.Managers;
using Jotunn.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RPGHeim
{
    [BepInPlugin("github.atravotum.rpgheim", "RPGHeim", "1.0.0")]
    [BepInDependency(Jotunn.Main.ModGuid)]

    internal class RPGHeimMain : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("github.atravotum.rpgheim");
        private static bool altKeyPressed = false;

        public static readonly List<StatusEffect> StatusEffects = new List<StatusEffect>();

        private void Awake()
        {
            // load in all teh required assets for the mod
            AssetManager.RegisterPrefabs();
            AssetManager.RegisterLocalization();
            SkillsManager.RegisterSkills();

            // run the harmony patches
            harmony.PatchAll();
        }

        private void Update()
        {
            // Since our Update function in our BepInEx mod class will load BEFORE Valheim loads,
            // we need to check that ZInput is ready to use first.
            if (ZInput.instance != null)
            {
                altKeyPressed = Input.GetKey(KeyCode.LeftAlt);

                if (Input.GetKeyDown(KeyCode.Alpha1) && altKeyPressed)
                {
                    AssetManager.ProjectileIndex++;
                    if (AssetManager.ProjectileIndex >= AssetManager.ProjectilesPrefabs.Count)
                    {
                        AssetManager.ProjectileIndex = 0;
                    }
                }
            }
        }

        void OnGUI() { RPGHeimHudSystem.Render(); }
    }
}