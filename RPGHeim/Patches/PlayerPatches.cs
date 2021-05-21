using HarmonyLib;
using RPGHeim.Managers;
using System;
using System.Linq;
using UnityEngine;

namespace RPGHeim
{
    // invoke various neccasary methods to prep the player for the RPGHeim mod/systems
    [HarmonyPatch(typeof(Player), "OnSpawned")]
    public static class RPGHeim_Player_Awake_Patch
    {
        private static void Postfix(ref Player __instance)
        {
            // check that we found a player and prep it for it's class
            if (__instance)
            {
                InputManager.Reset();

                InputManager.Awake();

                // Create the hotbar when we start the game.
                RPGHeimMain.UIHotBarManager.Toggle(true);

                // Trying to inject custom animations. --
                //var visualOfPlayer = Player.m_localPlayer.gameObject.transform.Find("Visual");
                //if(visualOfPlayer != null)
                //{
                //    var animatorOfPlayer = visualOfPlayer.GetComponent<Animator>();
                //    //Inject our animtor instead.
                //    var animationOverrideController = AssetManager.AssetBundles
                //        .FirstOrDefault(i => i.AssetBundleName == "animations");

                //    Jotunn.Logger.LogMessage("Trying to load...");

                //    if (animationOverrideController != null && animatorOfPlayer != null)
                //    {
                //        Jotunn.Logger.LogMessage("Assigning override controller.");
                //        var overrideController = animationOverrideController.AnimationOverrideControllers.FirstOrDefault();
                //        animatorOfPlayer.runtimeAnimatorController = overrideController.Config;
                //    }
                //}

                // Fighter prep
                float fighterLV = __instance.GetRPGHeimSkillFactor(SkillsManager.RPGHeimSkill.Fighter);
                if (fighterLV > 0) RPGHeimFighterClass.InitializePlayer(__instance, fighterLV);

                RPGHeimWizardClass.InitializePlayer(__instance, 100);
            }
        }
    }

    delegate void ActionRef<T>(Player player, ref T ___m_maxAirAltitude);

    // Token: 0x0200004C RID: 76
    [HarmonyPatch(typeof(Player), "Update", null)]
    public class AbilityInput_Postfix
    {
        // Token: 0x06000120 RID: 288 RVA: 0x0000C390 File Offset: 0x0000A590
        public static void Postfix(Player __instance, ref float ___m_maxAirAltitude)
        {
            Player localPlayer = Player.m_localPlayer;
            if (InputManager.ApplyAction)
            {
                InputManager.ApplyAction = false;
                switch (InputManager.Action)
                {
                    case InputManager.ActionToApply.Teleport:
                        RPGHeim.Modules.ClassSystem.Wizard.Abilities.Teleport.Execute(localPlayer, ref ___m_maxAirAltitude);
                        break;
                    case InputManager.ActionToApply.FrostNova:
                        RPGHeim.Modules.ClassSystem.Wizard.Abilities.FrostNova.Execute(localPlayer, ref ___m_maxAirAltitude);
                        break;
                    case InputManager.ActionToApply.Launch:
                        RPGHeim.Modules.ClassSystem.Wizard.Abilities.Launch.Execute(localPlayer, ref ___m_maxAirAltitude);
                        break;
                    default:
                        break;
                }

            }
        }
    }

    // intercepts hotbar item use and cancels if alt key is held (alt + 1-5 reserved for skills bar)
    [HarmonyPatch(typeof(Player), "UseHotbarItem")]
    public static class UseHotbarItemPrefix
    {
        public static void Prefix(ref int index)
        {
            if (!RPGHeimMain.UIHotBarManager.IsOverlayActive) { index = 0; return; }

            //Jotunn.Logger.LogMessage($"UseHotbarItem - altkey? {InputManager.AltKeyPressed} - {index}");
            if (InputManager.AltKeyPressed)
            {
                // Allow me to mod it? -- this is how we can restrict the main hotbar from getting hit.
                index = 0;
                //Jotunn.Logger.LogMessage($"UseHotbarItem restricted - {index}");
            }
        }
    }

    // Harmony patch to check when our mod's items are used so we can trigger effects
    [HarmonyPatch(typeof(Player), "ConsumeItem")]
    public static class RPGHeim_Player_ConsumeItem_Patch
    {
        private static void Postfix(ref Inventory inventory, ref ItemDrop.ItemData item, ref Player __instance)
        {
            if (item.IsRPGHeimItem())
            {
                RPGHeimItemsSystem.itemUsed(item, __instance);
            }
        }
    }
}
