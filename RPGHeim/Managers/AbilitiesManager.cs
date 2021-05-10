using Jotunn.Utils;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RPGHeim
{
    static class AbilitiesManager
    {
        private static AssetBundle WarriorIconBundle;
        private static AssetBundle WizardIconBundle;
        private static AssetBundle HealerIconBundle;
        private static AssetBundle RogueIconBundle;

        private static Dictionary<string, Ability> RegisteredAbilities = new Dictionary<string, Ability>();

        public static void RegisterAbilities()
        {
            // Load icon bundles
            LoadAssets();
            
            RegisterFighterAbilities();
            RegisterWizardAbilities();
            RegisterHealerAbilities();
            RegisterRogueAbilities();

            // unload icon bundles
            UnloadAssets();
        }

        private static void LoadAssets()
        {
            WarriorIconBundle = AssetUtils.LoadAssetBundleFromResources("warrioricons", Assembly.GetExecutingAssembly());
        }

        private static void UnloadAssets()
        {
            WarriorIconBundle.Unload(false);
        }

        private static void RegisterFighterAbilities()
        {
            Ability FightingSpirit = new Ability
            {
                Name = "$se_RPGHeimFightingSpirit",
                Tooltip = "$se_RPGHeimFightingSpirit_tooltip",
                Type = AbilityType.Passive,
                Icon = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Filled/SIW 8.png"),
                PassiveEffect = "SE_FightingSpirit",
                PassiveEffectTarget = AbilityTarget.Self
            };
            RegisteredAbilities.Add("FightingSpirit", FightingSpirit);

            Ability WarCry = new Ability
            {
                Name = "$se_RPGHeimWarCry",
                Tooltip = "$se_RPGHeimWarCry_tooltip",
                Type = AbilityType.Active,
                Icon = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Filled/SIW 4.png"),
                PassiveEffect = "SE_WarCry",
                PassiveEffectTarget = AbilityTarget.NearbyAllies
            };
            RegisteredAbilities.Add("WarCry", WarCry);

            Ability TrainedReflexes = new Ability
            {
                Name = "$se_RPGHeimTrainedReflexes",
                Tooltip = "$se_RPGHeimTrainedReflexes_tooltip",
                Type = AbilityType.Passive,
                Icon = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Filled/SIW 7.png"),
                PassiveEffect = "SE_TrainedReflexes",
                PassiveEffectTarget = AbilityTarget.Self
            };
            RegisteredAbilities.Add("TrainedReflexes", TrainedReflexes);

            Ability DualWielding = new Ability
            {
                Name = "$se_RPGHeimDualWielding",
                Tooltip = "$se_RPGHeimDualWielding_tooltip",
                Type = AbilityType.Passive,
                Icon = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Filled/SIW 1.png"),
                PassiveEffect = "SE_DualWielding",
                PassiveEffectTarget = AbilityTarget.Self
            };
            RegisteredAbilities.Add("DualWielding", DualWielding);

            Ability WeaponsMaster = new Ability
            {
                Name = "$se_RPGHeimWeaponsMaster",
                Tooltip = "$se_RPGHeimWeaponsMaster_tooltip",
                Type = AbilityType.Passive,
                Icon = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Filled/SIW 5.png"),
                PassiveEffect = "SE_WeaponsMaster",
                PassiveEffectTarget = AbilityTarget.Self
            };
            RegisteredAbilities.Add("WeaponsMaster", WeaponsMaster);
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

        public static Ability GetAbility (string abilityName)
        {
            Ability AbilityResult;
            RegisteredAbilities.TryGetValue(abilityName, out AbilityResult);
            return AbilityResult;
        }
    }
}
