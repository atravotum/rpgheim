using UnityEngine;
using HarmonyLib;

namespace RPGHeim
{
    internal class RPGHeimHudSystem : MonoBehaviour
    {
        private static bool isEnabled = false;
        public static ActionBar SkillsBar = new ActionBar
        {
            xPos = (Screen.width / 2) - 187,
            yPos = Screen.height - 150,
            width = 375,
            height = 75
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