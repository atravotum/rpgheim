using BepInEx;
using UnityEngine;
using HarmonyLib;
using Jotunn.Managers;

namespace RPGHeim
{
    [BepInPlugin("github.atravotum.rpgheim", "RPGHeim", "1.0.0")]
    [BepInDependency(Jotunn.Main.ModGuid)]

    internal class RPGHeimMain : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("github.atravotum.rpgheim");

        private void Awake()
        {
            AssetManager.RegisterPrefabs();
            AssetManager.RegisterLocalization();
            AssetManager.RegisterSkills();
            harmony.PatchAll();
        }

        /// Game tick updates - Check for custom inputs
        private void Update()
        {
            // Since our Update function in our BepInEx mod class will load BEFORE Valheim loads,
            // we need to check that ZInput is ready to use first.
            if (ZInput.instance != null)
            {
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha1))
                {
                    AssetManager.ProjectileIndex++;
                    if (AssetManager.ProjectileIndex >= AssetManager.ProjectilesPrefabs.Count)
                    {
                        AssetManager.ProjectileIndex = 0;
                    }
                }
            }
        }

        // invoke various neccasary methods to prep the player for the RPGHeim mod/systems
        [HarmonyPatch(typeof(Player), "Awake")]
        public static class RPGHeim_Player_Awake_Patch
        {
            private static void Postfix(ref Player __instance)
            {
                if (__instance)
                {
                    // Fighter prep
                    Skills.SkillDef fighterSkill = SkillManager.Instance.GetSkill("github.atravotum.rpgheim.skills.fighter");
                    float fighterLV = __instance.GetSkillFactor(fighterSkill.m_skill);
                    if (fighterLV > 0) RPGHeimFighterClass.InitializePlayer(__instance);
                }
            }
        }
    }
}