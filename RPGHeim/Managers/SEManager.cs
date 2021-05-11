using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System.Reflection;
using UnityEngine;

namespace RPGHeim
{
    public static class SEManager
    {
        private static AssetBundle WarriorIconBundle;
        private static AssetBundle WizardIconBundle;
        private static AssetBundle HealerIconBundle;
        private static AssetBundle RogueIconBundle;

        public static void RegisterStatusEffects ()
        {
            // Load icon bundles
            LoadAssets();

            // Fighter ability SEs
            SE_FightingSpirit();
            SE_WarCry();
            SE_TrainedReflexes();
            SE_DualWielding();
            SE_WeaponsMaster();

            // unload icon bundles
            UnloadAssets();
        }

        private static void LoadAssets ()
        {
            WarriorIconBundle = AssetUtils.LoadAssetBundleFromResources("warrioricons", Assembly.GetExecutingAssembly());
        }

        private static void UnloadAssets ()
        {
            WarriorIconBundle.Unload(false);
        }

        private static void SE_FightingSpirit ()
        {
            Texture Icon = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Transparent/SIW 8_1.png");
            SE_Stats NewSE = ScriptableObject.CreateInstance<SE_Stats>();
            NewSE.name = "SE_FightingSpirit";
            NewSE.m_name = "$se_RPGHeimFightingSpirit";
            NewSE.m_tooltip = "$se_RPGHeimFightingSpirit_description";
            NewSE.m_modifyAttackSkill = Skills.SkillType.All;
            NewSE.m_damageModifier = 100f;
            NewSE.m_addMaxCarryWeight = 75f;
            NewSE.m_icon = Sprite.Create((Texture2D)Icon, new Rect(0f, 0f, Icon.width, Icon.height), Vector2.zero);
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(NewSE, fixReference: false));
        }

        private static void SE_WarCry()
        {
            Texture Icon = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Transparent/SIW 4_1.png");
            SE_Stats NewSE = ScriptableObject.CreateInstance<SE_Stats>();
            NewSE.name = "SE_WarCry";
            NewSE.m_name = "$se_RPGHeimWarCry";
            NewSE.m_tooltip = "$se_RPGHeimWarCry_description";
            NewSE.m_staminaRegenMultiplier = 1.5f;
            NewSE.m_ttl = 15f;
            NewSE.m_icon = Sprite.Create((Texture2D)Icon, new Rect(0f, 0f, Icon.width, Icon.height), Vector2.zero);
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(NewSE, fixReference: false));
        }

        private static void SE_TrainedReflexes()
        {
            Texture Icon = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Transparent/SIW 7_1.png");
            SE_CustomModifier NewSE = ScriptableObject.CreateInstance<SE_CustomModifier>();
            NewSE.name = "SE_TrainedReflexes";
            NewSE.m_name = "$se_RPGHeimTrainedReflexes";
            NewSE.m_tooltip = "$se_RPGHeimTrainedReflexes_description";
            NewSE.m_blockModifier = 1.5f;
            NewSE.m_parryModifier = 1.5f;
            NewSE.m_icon = Sprite.Create((Texture2D)Icon, new Rect(0f, 0f, Icon.width, Icon.height), Vector2.zero);
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(NewSE, fixReference: false));
        }

        private static void SE_DualWielding()
        {
            Texture Icon = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Transparent/SIW 1_1.png");
            SE_Stats NewSE = ScriptableObject.CreateInstance<SE_Stats>();
            NewSE.name = "SE_DualWielding";
            NewSE.m_name = "$se_RPGHeimDualWielding";
            NewSE.m_tooltip = "$se_RPGHeimDualWielding_description";
            NewSE.m_icon = Sprite.Create((Texture2D)Icon, new Rect(0f, 0f, Icon.width, Icon.height), Vector2.zero);
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(NewSE, fixReference: false));
        }

        private static void SE_WeaponsMaster()
        {
            Texture Icon = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Transparent/SIW 5_1.png");
            SE_CustomModifier NewSE = ScriptableObject.CreateInstance<SE_CustomModifier>();
            NewSE.name = "SE_WeaponsMaster";
            NewSE.m_name = "$se_RPGHeimWeaponsMaster";
            NewSE.m_tooltip = "$se_RPGHeimWeaponsMaster_description";
            NewSE.m_modSkills.Add(Skills.SkillType.Axes, 100f);
            NewSE.m_modSkills.Add(Skills.SkillType.Blocking, 100f);
            NewSE.m_modSkills.Add(Skills.SkillType.Bows, 100f);
            NewSE.m_modSkills.Add(Skills.SkillType.Clubs, 100f);
            NewSE.m_modSkills.Add(Skills.SkillType.Knives, 100f);
            NewSE.m_modSkills.Add(Skills.SkillType.Polearms, 100f);
            NewSE.m_modSkills.Add(Skills.SkillType.Spears, 100f);
            NewSE.m_modSkills.Add(Skills.SkillType.Swords, 100f);
            NewSE.m_icon = Sprite.Create((Texture2D)Icon, new Rect(0f, 0f, Icon.width, Icon.height), Vector2.zero);
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(NewSE, fixReference: false));
        }
    }
}
