using Jotunn.Utils;
using System.Reflection;
using UnityEngine;
using HarmonyLib;

namespace RPGHeim
{
    internal class RPGHeimFighterClass : MonoBehaviour
    {
        public static void InitializePlayer(Player player, float skillLV)
        {
            // load the icons
            AssetBundle WarriorIconBundle = AssetUtils.LoadAssetBundleFromResources("warrioricons", Assembly.GetExecutingAssembly());
            Texture icon1Filled = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Filled/SIW 8.png");
            Texture icon1Trans = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Transparent/SIW 8_1.png");
            Texture icon2Filled = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Filled/SIW 4.png");
            Texture icon2Trans = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Transparent/SIW 4_1.png");
            Texture icon3Filled = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Filled/SIW 7.png");
            Texture icon3Trans = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Transparent/SIW 7_1.png");
            Texture icon4Filled = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Filled/SIW 1.png");
            Texture icon4Trans = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Transparent/SIW 1_1.png");
            Texture icon5Filled = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Filled/SIW 5.png");
            Texture icon5Trans = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Transparent/SIW 5_1.png");
            WarriorIconBundle.Unload(false);

            // create the fighting spirit passive ability (modifies are enforced in player patches)
            SE_Stats figherBuff1 = ScriptableObject.CreateInstance<SE_Stats>();
            figherBuff1.name = "SE_FightingSpirit";
            figherBuff1.m_damageModifier = 1.2f;
            figherBuff1.m_addMaxCarryWeight = 75f;
            figherBuff1.m_name = "$se_RPGHeimFightingSpirit";
            figherBuff1.m_tooltip = "$$se_RPGHeimFightingSpirit_description";
            figherBuff1.m_icon = Sprite.Create((Texture2D)icon1Trans, new Rect(0f, 0f, icon1Trans.width, icon1Trans.height), Vector2.zero);
            Ability fightingSpirit = new Ability { 
                name = "$se_RPGHeimFightingSpirit",
                tooltip = "$se_RPGHeimFightingSpirit_tooltip",
                type = "passive",
                icon = icon1Filled,
                passiveEffect = (StatusEffect)figherBuff1
            };
            RPGHeimHudSystem.SkillsBar.SetAbility(fightingSpirit, 0);
            player.m_seman.AddStatusEffect(figherBuff1);

            // create the fighters warcray ability
            SE_Stats figherBuff2 = ScriptableObject.CreateInstance<SE_Stats>();
            figherBuff2.name = "SE_WarCry";
            figherBuff1.m_staminaRegenMultiplier = 1.5f;
            figherBuff1.m_name = "$se_RPGHeimWarCry";
            figherBuff1.m_tooltip = "$se_RPGHeimWarCry_description";
            figherBuff1.m_icon = Sprite.Create((Texture2D)icon2Trans, new Rect(0f, 0f, icon2Trans.width, icon2Trans.height), Vector2.zero);
            Ability warCry = new Ability
            {
                name = "$se_RPGHeimWarCry",
                tooltip = "$se_RPGHeimWarCry_tooltip",
                type = "active",
                icon = icon2Filled,
                passiveEffect = (StatusEffect)figherBuff2
            };
            RPGHeimHudSystem.SkillsBar.SetAbility(warCry, 1);
        }
    }
}