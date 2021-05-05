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

        private void Awake()
        {
            harmony.PatchAll();
        }

        private static void CreateClassBar ()
        {
            Console.print("====================================OK OK OK OK OK OK OK +++++++++++++++++====================== HERE HRE HRE HRE HRE");
            var sprite = GUIManager.Instance.GetSprite("item_background");
            //var button = GUIManager.Instance.CreateButton("A Test Button", testPanel.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0, 0), 250, 100);
            //var text = GUIManager.Instance.CreateText("JötunnLib, the Valheim Lib", GUIManager.PixelFix.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 0f), GUIManager.Instance.AveriaSerifBold, 18, GUIManager.Instance.ValheimOrange, true, Color.black, 400f, 30f, false);
            var panel = GUIManager.Instance.CreateWoodpanel(GUIManager.PixelFix.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 0f), 400f, 300f);
            panel.SetActive(true);

        }

        // harmony patch to add a new hotkeybar to the players screen
        [HarmonyPatch(typeof(Hud), "Awake")]
        public static class Hud_Awake_Patch
        {
            public static void Postfix(Hud __instance)
            {
                /*HotkeyBar hotKeyBarChildComponent = __instance.GetComponentInChildren<HotkeyBar>();
                HotkeyBar testBar = new HotkeyBar();
                GameObject testBarGo = Object.Instantiate(hotKeyBarChildComponent.gameObject, __instance.m_healthBarRoot, worldPositionStays: true);
                GameObject testBarGO = testBar.GetComponent<GameObject>();
                testBarGO.name = "RPGHeimActionBar";
                (testBarGO.transform as RectTransform).anchoredPosition = new Vector2(55f, -500f);
                testBarGO.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
                testBarGO.GetComponent<HotkeyBar>().m_selected = -1;*/

                /*HotkeyBar hotKeyBarChildComponent = __instance.GetComponentInChildren<HotkeyBar>();
                if (hotKeyBarChildComponent.transform.parent.Find("RPGHeimQuickCastBar") == null)
                {
                    
                    newHotKeyBar.name = "hotKeyBarChildComponent";
                    (newHotKeyBar.transform as RectTransform).anchoredPosition = new Vector2(55f, -500f);
                    newHotKeyBar.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
                    newHotKeyBar.GetComponent<HotkeyBar>().m_selected = -1;

                    
                }*/
                CreateClassBar();
            }
        }
    }
}