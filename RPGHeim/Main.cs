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
                bool altKeyPressed = Input.GetKey(KeyCode.LeftAlt);
                if (altKeyPressed && Input.GetKeyDown(KeyCode.Alpha1))
                    RPGHeimHudSystem.SkillsBar.CastSlot(0, Player.m_localPlayer);
                else if (altKeyPressed && Input.GetKeyDown(KeyCode.Alpha2))
                    RPGHeimHudSystem.SkillsBar.CastSlot(1, Player.m_localPlayer);
                else if (altKeyPressed && Input.GetKeyDown(KeyCode.Alpha3))
                    RPGHeimHudSystem.SkillsBar.CastSlot(2, Player.m_localPlayer);
                else if (altKeyPressed && Input.GetKeyDown(KeyCode.Alpha4))
                    RPGHeimHudSystem.SkillsBar.CastSlot(3, Player.m_localPlayer);
                else if (altKeyPressed && Input.GetKeyDown(KeyCode.Alpha5))
                    RPGHeimHudSystem.SkillsBar.CastSlot(4, Player.m_localPlayer);

                // old projectile code probably needs to be moved to ability
                /*AssetManager.ProjectileIndex++;
                if (AssetManager.ProjectileIndex >= AssetManager.ProjectilesPrefabs.Count)
                {
                    AssetManager.ProjectileIndex = 0;
                }*/
            }
        }

        void OnGUI() { RPGHeimHudSystem.Render(); }
    }
}