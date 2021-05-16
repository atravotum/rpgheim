using Jotunn.Utils;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static RPGHeim.RPGHeimFighterClass;

namespace RPGHeim
{
    static class AbilitiesManager
    {
        private static Dictionary<string, Ability> RegisteredAbilities = new Dictionary<string, Ability>();

        public static void RegisterAbilities()
        {
            // Load icon bundles
            RegisterFighterAbilities();
            RegisterWizardAbilities();
            RegisterHealerAbilities();
            RegisterRogueAbilities();
        }

        private static void RegisterFighterAbilities()
        {
            Ability FightingSpirit = new Ability
            {
                Name = "$se_RPGHeimFightingSpirit",
                Tooltip = "$se_RPGHeimFightingSpirit_tooltip",
                Type = AbilityType.Passive,
                Icon = AssetManager.LoadSpriteFromBundle("warrioricons", "Assets/Skill icons Warrior/Icons/Filled/SIW 2.png"),
                PassiveEffect = "SE_FightingSpirit",
                PassiveEffectTarget = AbilityTarget.Self
            };
            RegisteredAbilities.Add(FighterAbilities.FightingSpirit, FightingSpirit);

            Ability WarCry = new Ability
            {
                Name = "$se_RPGHeimWarCry",
                Tooltip = "$se_RPGHeimWarCry_tooltip",
                Type = AbilityType.Active,
                Icon = AssetManager.LoadSpriteFromBundle("warrioricons", "Assets/Skill icons Warrior/Icons/Filled/SIW 4.png"),
                StaminaCost = 0.25f, // values between 0.01 and .99 are converted to a percentage, otherwise flat value
                CooldownMax = 60f,
                PassiveEffect = "SE_WarCry",
                PassiveEffectTarget = AbilityTarget.NearbyAllies
            };
            RegisteredAbilities.Add(FighterAbilities.WarCry, WarCry);

            Ability TrainedReflexes = new Ability
            {
                Name = "$se_RPGHeimTrainedReflexes",
                Tooltip = "$se_RPGHeimTrainedReflexes_tooltip",
                Type = AbilityType.Passive,
                Icon = AssetManager.LoadSpriteFromBundle("warrioricons", "Assets/Skill icons Warrior/Icons/Filled/SIW 7.png"),
                PassiveEffect = "SE_TrainedReflexes",
                PassiveEffectTarget = AbilityTarget.Self
            };
            RegisteredAbilities.Add(FighterAbilities.TrainedReflexes, TrainedReflexes);

            Ability DualWielding = new Ability
            {
                Name = "$se_RPGHeimDualWielding",
                Tooltip = "$se_RPGHeimDualWielding_tooltip",
                Type = AbilityType.Passive,
                Icon = AssetManager.LoadSpriteFromBundle("warrioricons", "Assets/Skill icons Warrior/Icons/Filled/SIW 1.png"),
                PassiveEffect = "SE_DualWielding",
                PassiveEffectTarget = AbilityTarget.Self
            };
            RegisteredAbilities.Add(FighterAbilities.DualWielding, DualWielding);

            Ability StrengthWielding = new Ability
            {
                Name = "$se_RPGHeimStrengthWielding",
                Tooltip = "$se_RPGHeimStrengthWielding_tooltip",
                Type = AbilityType.Passive,
                Icon = AssetManager.LoadSpriteFromBundle("warrioricons", "Assets/Skill icons Warrior/Icons/Filled/SIW 8.png"),
                PassiveEffect = "SE_StrengthWielding",
                PassiveEffectTarget = AbilityTarget.Self
            };
            RegisteredAbilities.Add(FighterAbilities.StrengthWielding, StrengthWielding);

            Ability WeaponsMaster = new Ability
            {
                Name = "$se_RPGHeimWeaponsMaster",
                Tooltip = "$se_RPGHeimWeaponsMaster_tooltip",
                Type = AbilityType.Passive,
                Icon = AssetManager.LoadSpriteFromBundle("warrioricons", "Assets/Skill icons Warrior/Icons/Filled/SIW 5.png"),
                PassiveEffect = "SE_WeaponsMaster",
                PassiveEffectTarget = AbilityTarget.Self
            };
            RegisteredAbilities.Add(FighterAbilities.WeaponsMaster, WeaponsMaster);

            // Cleanup.
            AssetManager.UnloadAssetBundles();
        }

        private static void RegisterWizardAbilities()
        {

        }

        private static void RegisterHealerAbilities()
        {

        }

        private static void RegisterRogueAbilities()
        {

        }

        public static Ability GetAbility(string abilityName)
        {
            return RegisteredAbilities[abilityName];
        }
    }
}
