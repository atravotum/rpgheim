using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPGHeim.Managers
{
    public static class InputManager
    {
        public enum ActionToApply
        {
            Teleport,
            FrostNova,
            Launch
        }

        public static bool ApplyAction = false;
        public static ActionToApply Action { get; set; }

        public static bool HotbarEnabled { get; set; } = false;

        public static bool AltKeyPressed = false;
        public static bool MouseRightClicked = false;
        public static bool MouseLeftClicked = false;
        public static Vector3 MousePosition { get; set; }
        public static bool MapIsOpen { get; set; } = false;
        public static bool MenuWindowIsOpen { get; set; } = false;

        public static int ActiveHotbarIndex = 0;

        public static void Reset()
        {
            AltKeyPressed = false;
            MapIsOpen = false;
            MenuWindowIsOpen = false;
            MouseRightClicked = false;
            MouseLeftClicked = false;
        }

        public static List<UIHotBarManager> HotBars = new List<UIHotBarManager>();

        public static void Awake()
        {
            if (HotBars.Count == 0)
            {

                // Setup all the hotbars 1-4?
                for (int i = 0; i < 5; i++)
                {
                    // New hotbar!
                    var hotbar = new UIHotBarManager();
                    hotbar.CreateHotBar(i);
                    hotbar.IsOverlayActive = true;
                    HotBars.Add(hotbar);
                    hotbar.Deactivate();
                    Jotunn.Logger.LogMessage($"Hotbar registered - {i}");
                }
                RPGHeimMain.UIHotBarManager = HotBars[0];
                //RPGHeimMain.UIHotBarManager.Toggle(false);
            }
        }

        public static void Update()
        {
            if (ZInput.instance != null)
            {
                try
                {
                    AltKeyPressed = Input.GetKey(KeyCode.LeftAlt);

                    #region -- Checking input to close Skill Window on certain conditions.
                    if (Input.GetKeyDown(KeyCode.Tab))
                    {
                        if (!MapIsOpen && !MenuWindowIsOpen)
                        {
                            RPGHeimMain.UIAbilityWindowManager.Toggle(shutWindow: true);
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.M))
                    {
                        if (!RPGHeimMain.UIAbilityWindowManager.IsActive && !MenuWindowIsOpen)
                        {
                            MapIsOpen = !MapIsOpen;
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        if (MapIsOpen)
                        {
                            MapIsOpen = !MapIsOpen;
                        }
                        else if (!MapIsOpen && RPGHeimMain.UIAbilityWindowManager.IsActive)
                        {
                            MenuWindowIsOpen = !MenuWindowIsOpen;
                        }
                        RPGHeimMain.UIAbilityWindowManager.Toggle(shutWindow: true);
                    }

                    #endregion -- Checking input to close Skill Window on certain conditions.

                    if (!AltKeyPressed && Input.GetKeyDown(KeyCode.Alpha9))
                    {
                        ApplyAction = true;
                        Action = ActionToApply.FrostNova;
                    }

                    if (!AltKeyPressed && Input.GetKeyDown(KeyCode.Alpha0))
                    {
                        ApplyAction = true;
                        Action = ActionToApply.Teleport;
                    }

                    if (AltKeyPressed && Input.GetKeyDown(KeyCode.Alpha0))
                    {
                        ApplyAction = true;
                        Action = ActionToApply.Launch;
                    }

                    UIHotBarManager.AbilityButton skillAtSlot = null;
                    Ability abilityToCast = null;

                    var keyCode = KeyCode.Alpha1;
                    for (int i = 0; i < 5; i++)
                    {
                        // Check if any of the alpha keys are hit.
                        if (Input.GetKeyDown(keyCode))
                        {
                            if (AltKeyPressed)
                            {
                                var hotbar = HotBars[i];
                                hotbar.Toggle(true);
                                Jotunn.Logger.LogMessage($"Hotbar selected.. - {i}");
                                if (ActiveHotbarIndex == i)
                                {
                                    Jotunn.Logger.LogMessage($"Toggle hotbar - {hotbar.Window.activeSelf}");
                                    hotbar.IsOverlayActive = !hotbar.IsOverlayActive;
                                    //RPGHeimMain.UIHotBarManager.IsActive = !RPGHeimMain.UIHotBarManager.IsActive;
                                    Jotunn.Logger.LogMessage($"Toggle IsOverlayActive - {hotbar.IsOverlayActive}");
                                }
                                else
                                {
                                    Jotunn.Logger.LogMessage($"Toggle new hotbar - {i}");
                                    hotbar.IsOverlayActive = RPGHeimMain.UIHotBarManager.IsOverlayActive;
                                    RPGHeimMain.UIHotBarManager.Deactivate();
                                    RPGHeimMain.UIHotBarManager = hotbar;
                                    Jotunn.Logger.LogMessage($"Toggle new hotbar - {i}");
                                }
                                ActiveHotbarIndex = i;
                            }
                            else
                            {
                                // If the hotbar is active, then we will register the ability clicks.
                                if (RPGHeimMain.UIHotBarManager.IsOverlayActive) return;

                                //Jotunn.Logger.LogMessage($"Detected key hit! - {keyCode}");
                                skillAtSlot = RPGHeimMain.UIHotBarManager.AbilityButtons[i];
                                if (skillAtSlot != null)
                                {
                                    if (skillAtSlot.ability != null)
                                    {
                                        Jotunn.Logger.LogMessage($"{skillAtSlot.ability.Name}");
                                        skillAtSlot.ability.CastAbility();
                                    }
                                    else if (skillAtSlot.dragSlot != null)
                                    {
                                        Jotunn.Logger.LogMessage($"No Ability in the slot! empty: {skillAtSlot.dragSlot != null && skillAtSlot.dragSlot.IsEmpty}");
                                    }
                                    else
                                    {
                                        Jotunn.Logger.LogMessage($"No Ability in the slot! - null?: {skillAtSlot.dragSlot != null}");
                                    }
                                }
                            }
                        }
                        // Check alpha2,3,4,5.. etc.
                        keyCode = (KeyCode)(int)(keyCode + 1);
                    }
                }
                catch (Exception ex)
                {
                    Jotunn.Logger.LogMessage($"Input Manager failed -- {ex}");
                }
            }
        }
    }
}
