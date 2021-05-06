using BepInEx;
using UnityEngine;
using HarmonyLib;
using Object = UnityEngine.Object;
using Jotunn.Managers;

namespace RPGHeim
{
    [BepInPlugin("github.atravotum.rpgheim.hud", "RPGHeim", "1.0.0")]
    [BepInDependency(Jotunn.Main.ModGuid)]
    internal class RPGHeimHudSystem : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("github.atravotum.rpgheim");
        public static ActionBar newActionBar = new ActionBar
        {
            xPos = (Screen.width / 2) - 187,
            yPos = Screen.height - 150,
            width = 375,
            height = 75
        };

        private void Awake()
        {
            harmony.PatchAll();
        }

        void OnGUI()
        {
            newActionBar.Render();
        }

        private static void InitializeActionBar ()
        {
            RPGHeimFighterClass.PrepActionBar(newActionBar);
        }

        // harmony patch to add a new hotkeybar to the players screen
        [HarmonyPatch(typeof(Hud), "Awake")]
        public static class Hud_Awake_Patch
        {
            public static void Postfix(Hud __instance)
            {
                InitializeActionBar();
            }
        }
    }
}