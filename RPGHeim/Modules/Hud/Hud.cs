using UnityEngine;
using HarmonyLib;
using System;

namespace RPGHeim
{
    internal class RPGHeimHudSystem : MonoBehaviour
    {
        public static bool isEnabled = false;
        public static ActionBar SkillsBar = new ActionBar
        {
            xPos = 100,
            yPos = (int)Math.Round(Screen.height - (Screen.height / 20 * 1.5)),
            width = (Screen.height / 20) * 5,
            height = (Screen.height / 20)
        };
        
        public static void Start()
        {
            isEnabled = true;
        }


        public static void Render()
        {
            if (isEnabled)
            {
                SkillsBar.Render();
            }
        }

        public void Enable() => isEnabled = true;
        public void Disable() => isEnabled = false;
    }

    // Harmony patch to check when our mod's items are used so we can trigger effects
    [HarmonyPatch(typeof(Hud), "Awake")]
    public static class RPGHeim_Hud_Awake_Patch
    {
        private static void Postfix()
        {
            // start up the hud system
            RPGHeimHudSystem.Start();
        }
    }
}