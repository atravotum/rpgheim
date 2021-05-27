using BepInEx;
using UnityEngine;
using HarmonyLib;
using System;

namespace RPGHeim
{
    [BepInPlugin("github.atravotum.rpgheim", "RPGHeim", "1.0.0")]
    [BepInDependency(Jotunn.Main.ModGuid)]

    internal class RPGHeimMain : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("github.atravotum.rpgheim");
        
        public static ActionBar SkillsBar = new ActionBar
        {
            xPos = 100,
            yPos = (int)Math.Round(Screen.height - (Screen.height / 20 * 1.5)),
            width = (Screen.height / 20) * 5,
            height = (Screen.height / 20)
        };

        private void Awake()
        {
            // load in all the required assets for the mod
            AssetManager.LoadAssets();

            // run the harmony patches
            harmony.PatchAll();

            // invoke the render to repeat every 1 seconds
            InvokeRepeating("TickCooldowns", 0f, SkillsBar.cooldownTickRate);
            InvokeRepeating("TickPassives", 0f, 2f);
        }

        private void Update()
        {
            // Check for instance of zinput
            if (ZInput.instance != null)
            {
                // check if the alt key was pressed in combination with 1-5, if so cast ability
                bool altKeyPressed = Input.GetKey(KeyCode.LeftAlt);
                if (altKeyPressed && Input.GetKeyDown(KeyCode.Alpha1))
                    SkillsBar.CastSlot(0, Player.m_localPlayer);
                else if (altKeyPressed && Input.GetKeyDown(KeyCode.Alpha2))
                    SkillsBar.CastSlot(1, Player.m_localPlayer);
                else if (altKeyPressed && Input.GetKeyDown(KeyCode.Alpha3))
                    SkillsBar.CastSlot(2, Player.m_localPlayer);
                else if (altKeyPressed && Input.GetKeyDown(KeyCode.Alpha4))
                    SkillsBar.CastSlot(3, Player.m_localPlayer);
                else if (altKeyPressed && Input.GetKeyDown(KeyCode.Alpha5))
                    SkillsBar.CastSlot(4, Player.m_localPlayer);
            }
        }

        void OnGUI() { SkillsBar.Render(); }

        private void TickCooldowns() { SkillsBar.TickCooldowns(); }
        private void TickPassives() { SkillsBar.TickPassives(); }
    }
}