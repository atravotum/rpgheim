using Jotunn.Utils;
using System.Reflection;
using UnityEngine;

namespace RPGHeim
{
    internal class RPGHeimFighterClass : MonoBehaviour
    {
        public static void InitializePlayer(Player player, float skillLV)
        {
            // Add the proper fighter skills to the skills bar
            RPGHeimHudSystem.SkillsBar.SetAbility(AbilitiesManager.GetAbility("FightingSpirit"), 0);
            RPGHeimHudSystem.SkillsBar.SetAbility(AbilitiesManager.GetAbility("WarCry"), 1);
            RPGHeimHudSystem.SkillsBar.SetAbility(AbilitiesManager.GetAbility("TrainedReflexes"), 2);
            RPGHeimHudSystem.SkillsBar.SetAbility(AbilitiesManager.GetAbility("DualWielding"), 3);
            RPGHeimHudSystem.SkillsBar.SetAbility(AbilitiesManager.GetAbility("WeaponsMaster"), 4);
            RPGHeimHudSystem.SkillsBar.ActivatePassiveAbilities();
        }
    }
}