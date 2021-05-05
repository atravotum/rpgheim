using BepInEx;
using UnityEngine;
using HarmonyLib;
using Object = UnityEngine.Object;

namespace RPGHeim
{
    internal class RPGHeimHudSystem : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("github.atravotum.rpgheim");

        private void Awake()
        {
            harmony.PatchAll();
        }

        // harmony patch to add a new hotkeybar to the players screen
        [HarmonyPatch(typeof(Hud), "Awake")]
        public static class Hud_Awake_Patch
        {
            public static void Postfix(Hud __instance)
            {
                HotkeyBar hotKeyBarChildComponent = __instance.GetComponentInChildren<HotkeyBar>();
                if (hotKeyBarChildComponent.transform.parent.Find("RPGHeimQuickCastBar") == null)
                {
                    GameObject newHotKeyBar = Object.Instantiate(hotKeyBarChildComponent.gameObject, __instance.m_healthBarRoot, worldPositionStays: true);
                    newHotKeyBar.name = "hotKeyBarChildComponent";
                    (newHotKeyBar.transform as RectTransform).anchoredPosition = new Vector2(55f, -500f);
                    newHotKeyBar.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
                    newHotKeyBar.GetComponent<HotkeyBar>().m_selected = -1;

                    foreach (HotkeyBar.ElementData element2 in newHotKeyBar.GetComponent<HotkeyBar>().m_elements)
                    {
                        Console.print("kk should do it now");
                        if (element2 != null) Console.print(element2);
                        Object.Destroy(element2.m_go);
                    }
                }
            }
        }
    }
}