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
        public static bool AltKeyPressed = false;
        public static bool MouseRightClicked = false;
        public static bool MouseLeftClicked = false;
        public static Vector3 MousePosition { get; set; }
        public static bool MapIsOpen { get; set; } = false;
        public static bool MenuWindowIsOpen { get; set; } = false;

        public static void Update()
        {
            if (ZInput.instance != null)
            {
                try
                {
                    AltKeyPressed = Input.GetKey(KeyCode.LeftAlt);

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

                    if (InputManager.AltKeyPressed)
                    {
                        UIHotBarManager.AbilityButton skillAtSlot = null;
                        Ability abilityToCast = null;

                        var keyCode = KeyCode.Alpha1;
                        for (int i = 0; i < 5; i++)
                        {
                            // Check if any of the alpha keys are hit.
                            if (Input.GetKeyDown(keyCode))
                            {
                                //Jotunn.Logger.LogMessage($"Detected key hit! - {keyCode}");
                                skillAtSlot = RPGHeimMain.UIHotBarManager.AbilityButtons[i];
                                //abilityToCast = UIHotBarManager.CurrentAbilities[i];

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

                                //try
                                //{

                                //    if (abilityToCast != null)
                                //    {
                                //        Jotunn.Logger.LogMessage($"Casting: {skillAtSlot.dragSlot != null}");
                                //        abilityToCast.CastAbility();
                                //    }
                                //}
                                //catch (Exception ex)
                                //{
                                //    Jotunn.Logger.LogMessage($"failed to cast -- ");
                                //}
                            }
                            // Check alpha2,3,4,5.. etc.
                            keyCode = (KeyCode)(int)(keyCode + 1);
                        }                        
                    }
                }
                catch (Exception ex)
                {
                    Jotunn.Logger.LogMessage("Input Manager failed -- ");
                }
            }
        }
    }
}
