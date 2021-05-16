using Jotunn.Utils;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RPGHeim
{
    internal class RPGHeimFighterClass : MonoBehaviour
    {
        public class FighterAbilities
        {
            public static string FightingSpirit { get; set; } = "FightingSpirit";
            public static string WarCry { get; set; } = "WarCry";
            public static string DualWielding { get; set; } = "DualWielding";
            public static string TrainedReflexes { get; set; } = "TrainedReflexes";
            public static string StrengthWielding { get; set; } = "StrengthWielding";
            public static string WeaponsMaster { get; set; } = "WeaponsMaster";

        }

        public static List<Ability> AllFighterAbilities = new List<Ability>();
        public static List<AbilitySlot> FighterAbilitiesAsSlots = new List<AbilitySlot>();

        public static void InitializePlayer(Player player, float skillLV)
        {
            // Reflection over the FighterAbilities strings.
            var instance = new FighterAbilities();
            foreach (PropertyInfo prop in instance.GetType().GetProperties())
            {
                var abilityName = prop.GetValue(instance).ToString();
                var ability = AbilitiesManager.GetAbility(abilityName);
                AllFighterAbilities.Add(ability);
                FighterAbilitiesAsSlots.Add(new AbilitySlot()
                {
                    Name = abilityName,
                    Icon = ability.Icon,
                    Description = "Something to be added.",
                    ClassType = "Fighter",
                    Ability = ability
                });
            }

            //Set Abiltiies Windows to fighter specific.
            //RPGHeimMain.UIAbilityWindowManager.AbilitySlots.AddRange(FighterAbilitiesAsSlots);
            RPGHeimMain.UIAbilityWindowManager.SetAvailableAbilities(FighterAbilitiesAsSlots);

            Jotunn.Logger.LogMessage($"found {AllFighterAbilities.Count} abilities");

            //// Add the proper fighter skills to the skills bar
            //RPGHeimHudSystem.SkillsBar.SetAbility(AbilitiesManager.GetAbility(FighterAbilities.FightingSpirit), 0);
            //RPGHeimHudSystem.SkillsBar.SetAbility(AbilitiesManager.GetAbility(FighterAbilities.WarCry), 1);
            //RPGHeimHudSystem.SkillsBar.SetAbility(AbilitiesManager.GetAbility(FighterAbilities.TrainedReflexes), 2);
            //RPGHeimHudSystem.SkillsBar.SetAbility(AbilitiesManager.GetAbility(FighterAbilities.StrengthWielding), 3);
            //RPGHeimHudSystem.SkillsBar.SetAbility(AbilitiesManager.GetAbility(FighterAbilities.WeaponsMaster), 4);

            //RPGHeimHudSystem.SkillsBar.ActivatePassiveAbilities();
        }
    }
}