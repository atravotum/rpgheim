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
            // load the skill icons in the skill bar
            AssetBundle WarriorIconBundle = AssetUtils.LoadAssetBundleFromResources("warrioricons", Assembly.GetExecutingAssembly());
            Texture icon0Filled = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Filled/SIW 8.png");
            RPGHeimHudSystem.SkillsBar.UpdateIconImg(0, icon0Filled);

            // apply the warrior LV 1 passive
            Texture icon0Trans = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Transparent/SIW 8_1.png");
            SE_Stats figherBuff1 = ScriptableObject.CreateInstance<SE_Stats>();
            figherBuff1.name = "figherBuff1";
            figherBuff1.m_damageModifier = 1.2f;
            figherBuff1.m_addMaxCarryWeight = 75f;
            figherBuff1.m_name = "$se_RPGHeimHouseOfGains";
            figherBuff1.m_tooltip = "$se_RPGHeimHouseOfGains_description";
            figherBuff1.m_icon = Sprite.Create((Texture2D)icon0Trans, new Rect(0f, 0f, icon0Trans.width, icon0Trans.height), Vector2.zero);
            player.m_seman.AddStatusEffect(figherBuff1);

            // unload the icon bundle
            WarriorIconBundle.Unload(false);
        }
    }
}