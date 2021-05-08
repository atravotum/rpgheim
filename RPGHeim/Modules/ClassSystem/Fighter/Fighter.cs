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

    [HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyMaxCarryWeight))]
    public static class AddCarryWeight_SEMan_ModifyMaxCarryWeight_Patch
    {
        public static void Postfix(SEMan __instance, ref float limit)
        {
            SE_Stats fighterBuff = (SE_Stats)Player.m_localPlayer.m_seman.GetStatusEffect("figherBuff1");
            if (fighterBuff) limit += fighterBuff.m_addMaxCarryWeight;
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
                hit.ApplyModifier(1.2f);
            }

            return true;
        }
    }
}