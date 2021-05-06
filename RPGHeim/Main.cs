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
            harmony.PatchAll();

            AssetManager.RegisterPrefabs();
            AssetManager.RegisterLocalization();
            AssetManager.RegisterSkills();
            
            // create a new action bar for the fighter skills
            //ActionBar newActionBar = new ActionBar();
            //newActionBar.CreateSlot();
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
        /* [HarmonyPatch(typeof(Player), "Awake")]
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
        }*/

        // Harmony patch to check when our mod's items are used so we can trigger effects
        [HarmonyPatch(typeof(Player), "ConsumeItem")]
        public static class RPGHeimItemUsed
        {
            private static void Postfix(ref Inventory inventory, ref ItemDrop.ItemData item, ref Player __instance)
            {
                Console.print("Ok I'm in the postfix patch...");
                if (item.m_shared.m_name.Contains("RPGHeim"))
                {
                    Console.print("Ok item is an RPGHeim item, going to invoke the method...");
                    RPGHeimItemsSystem.itemUsed(item, __instance);
                }
            }
        }
    }
}