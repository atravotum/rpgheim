using HarmonyLib;
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
                // Fighter prep
                Skills.SkillDef fighterSkill = SkillsManager.GetSkill(SkillsManager.RPGHeimSkill.Fighter);
                float fighterLV = __instance.GetSkillFactor(fighterSkill.m_skill);
                if (fighterLV > 0) RPGHeimFighterClass.InitializePlayer(__instance, fighterLV);
            }
        }
    }

    [HarmonyPatch(typeof(Humanoid), "StartAttack")]
    public class VL_Damage_Patch
    {
        public static bool PreFix(Humanoid __instance)
        {
            Console.print("Ok Humanoid started attack");
            SE_Stats fighterBuff = (SE_Stats)__instance.m_seman.GetStatusEffect("SE_FightingSpirit");
            if (fighterBuff)
            {
                Console.print("Ok Humanoid has the fighter buff lets do this!");
                __instance.m_currentAttack.m_damageMultiplier = 100f;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(Player), "StartAttack")]
    public class VL_Damage_Patch2
    {
        public static bool PreFix(Player __instance)
        {
            Console.print("Ok player started attack");
            SE_Stats fighterBuff = (SE_Stats)__instance.m_seman.GetStatusEffect("SE_FightingSpirit");
            if (fighterBuff)
            {
                Console.print("Ok player has the fighter buff lets do this!");
                __instance.m_currentAttack.m_damageMultiplier = 100f;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(Player), "UseHotbarItem")]
    public static class UseHotbarItemPrefix
    {
        public static void Prefix(ref int index)
        {
            bool altKeyPressed = Input.GetKey(KeyCode.LeftAlt);
            Jotunn.Logger.LogMessage($"UseHotbarItem - altkey? {altKeyPressed} - {index}");
            if (altKeyPressed)
            {
                // Allow me to mod it?
                index = 0;
                Jotunn.Logger.LogMessage($"UseHotbarItem restricted - {index}");
            }
        }
    }

    // Harmony patch to check when our mod's items are used so we can trigger effects
    [HarmonyPatch(typeof(Player), "ConsumeItem")]
    public static class RPGHeim_Player_ConsumeItem_Patch
    {
        private static void Postfix(ref Inventory inventory, ref ItemDrop.ItemData item, ref Player __instance)
        {
            if (item.m_shared.m_name.Contains("RPGHeim"))
            {
                RPGHeimItemsSystem.itemUsed(item, __instance);
            }
        }
    }
}
