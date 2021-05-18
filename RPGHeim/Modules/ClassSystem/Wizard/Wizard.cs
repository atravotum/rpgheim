using Jotunn.Utils;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RPGHeim
{
    internal class RPGHeimWizardClass : MonoBehaviour
    {
        public class WizardAbilities
        {
            public static string MagicMissile { get; set; } = "Magic Missile";
            public static string Firebolt { get; set; } = "Firebolt";
            public static string Fireball { get; set; } = "Fireball";
            public static string Magmablast { get; set; } = "Magmablast";
            public static string Waterblast { get; set; } = "Waterblast";
            public static string LightningBlast  { get; set; } = "Lightningblast";
        }

        public static List<Ability> AllAbilities = new List<Ability>();
        public static List<AbilitySlot> AbilitiesAsSlots = new List<AbilitySlot>();

        public static void InitializePlayer(Player player, float skillLV)
        {
            // Reflection over the FighterAbilities strings.
            var instance = new WizardAbilities();
            foreach (PropertyInfo prop in instance.GetType().GetProperties())
            {
                var abilityName = prop.GetValue(instance).ToString();
                var ability = AbilitiesManager.GetAbility(abilityName);
                AllAbilities.Add(ability);
                AbilitiesAsSlots.Add(new AbilitySlot()
                {
                    Name = abilityName,
                    Icon = ability.Icon,
                    Description = ability.Tooltip,
                    ClassType = "Wizard",
                    Ability = ability
                });
            }

            //Set Abiltiies Windows to fighter specific.
            //RPGHeimMain.UIAbilityWindowManager.AbilitySlots.AddRange(FighterAbilitiesAsSlots);
            RPGHeimMain.UIAbilityWindowManager.SetAvailableAbilities(AbilitiesAsSlots);

            Jotunn.Logger.LogMessage($"found {AllAbilities.Count} abilities");

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