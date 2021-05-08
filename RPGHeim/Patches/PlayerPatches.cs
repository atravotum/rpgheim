using HarmonyLib;

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

    [HarmonyPatch(typeof(Character), "Damage", null)]
    public class VL_Damage_Patch
    {
        public static bool Prefix(Character __instance, ref HitData hit, float ___m_maxAirAltitude)
        {
            SE_Stats fighterBuff = (SE_Stats)Player.m_localPlayer.m_seman.GetStatusEffect("figherBuff1");
            if (fighterBuff && __instance == Player.m_localPlayer)
            {
                hit.ApplyModifier(fighterBuff.m_damageModifier);
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(Player), "UseHotbarItem")]
    public static class UseHotbarItemPrefix
    {
        public static void Prefix(ref int index)
        {
            /*Jotunn.Logger.LogMessage($"UseHotbarItem - altkey? {altKeyPressed} - {index}");
            if (altKeyPressed)
            {
                // Allow me to mod it?
                index = 0;
                Jotunn.Logger.LogMessage($"UseHotbarItem restricted - {index}");
            }*/
        }
    }
}
