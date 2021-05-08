using BepInEx;
using UnityEngine;
using HarmonyLib;
using Jotunn.Managers;
using Jotunn.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Jotunn.Configs;

namespace RPGHeim
{
    [BepInPlugin("github.atravotum.rpgheim", "RPGHeim", "1.0.0")]
    [BepInDependency(Jotunn.Main.ModGuid)]

    internal class RPGHeimMain : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("github.atravotum.rpgheim");
        private static bool altKeyPressed = false;

        public static readonly List<StatusEffect> StatusEffects = new List<StatusEffect>();

        private void Awake()
        {
            // load in all teh required assets for the mod
            AssetManager.RegisterPrefabs();
            AssetManager.RegisterLocalization();

            SkillsManager.RegisterSkills();

            var customEffect = new CustomStatusEffect(ScriptableObject.CreateInstance(typeof(SE_CustomEffect)) as SE_CustomEffect, fixReference: false);
            StatusEffects.Add(customEffect.StatusEffect);
            ItemManager.Instance.AddStatusEffect(customEffect);

            // run the harmony patches
            harmony.PatchAll();
        }

        private void Update()
        {
            // Since our Update function in our BepInEx mod class will load BEFORE Valheim loads,
            // we need to check that ZInput is ready to use first.
            if (ZInput.instance != null)
            {
                altKeyPressed = Input.GetKey(KeyCode.LeftAlt);

                if (Input.GetKeyDown(KeyCode.Alpha1) && altKeyPressed)
                {
                    AssetManager.ProjectileIndex++;
                    if (AssetManager.ProjectileIndex >= AssetManager.ProjectilesPrefabs.Count)
                    {
                        AssetManager.ProjectileIndex = 0;
                    }
                }
            }
        }

        void OnGUI() { RPGHeimHudSystem.Render(); }

        // invoke various neccasary methods to prep the player for the RPGHeim mod/systems
        [HarmonyPatch(typeof(Player), "Awake")]
        public static class RPGHeim_Player_Awake_Patch
        {
            private static void Postfix(ref Player __instance)
            {
                // check that we found a player and prep it for it's class
                if (__instance)
                {
                    // Fighter prep
                    Skills.SkillDef fighterSkill = RPGHeim.SkillsManager.GetSkill(SkillsManager.RPGHeimSkill.Fighter);
                    float fighterLV = __instance.GetSkillFactor(fighterSkill.m_skill);
                    if (fighterLV > 0) RPGHeimFighterClass.InitializePlayer(__instance, fighterLV);

                    __instance.m_seman.AddStatusEffect(StatusEffects.FirstOrDefault(), true);
                }
            }
        }

        //[HarmonyPatch(typeof(Player), "TakeInput")]
        //public static class TakeInputPrefix
        //{
        //    public static bool Prefix()
        //    {
        //        //Jotunn.Logger.LogMessage($"TakeInput - altkey? {altKeyPressed}");
        //        if (altKeyPressed)
        //        {
        //            //Jotunn.Logger.LogMessage($"TakeInput restricted");
        //            return false;
        //        }

        //        return true;
        //    }
        //}

        [HarmonyPatch(typeof(Player), "UseHotbarItem")]
        public static class UseHotbarItemPrefix
        {
            public static void Prefix(ref int index)
            {
                Jotunn.Logger.LogMessage($"UseHotbarItem - altkey? {altKeyPressed} - {index}");
                if (altKeyPressed)
                {
                    // Allow me to mod it?
                    index = 0;
                    Jotunn.Logger.LogMessage($"UseHotbarItem restricted - {index}");
                }
            }
        }

        [HarmonyPatch(typeof(Player), "ActivateGuardianPower", null)]
        public class ActivatePowerPrevention_Patch
        {
            public static bool Prefix(Player __instance, ref bool __result)
            {
                bool result;
                if (altKeyPressed)
                {
                    __result = false;
                    result = false;
                }
                else
                {
                    result = true;
                }
                return result;
            }
        }
    }
}