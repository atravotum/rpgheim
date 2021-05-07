using Jotunn.Utils;
using System.Reflection;
using UnityEngine;

namespace RPGHeim
{
    internal class RPGHeimFighterClass : MonoBehaviour
    {
        public static void InitializePlayer(Player player, float skillLV)
        {
            // load the skill icons in the skill bar
            AssetBundle WarriorIconBundle = AssetUtils.LoadAssetBundleFromResources("warrioricons", Assembly.GetExecutingAssembly());
            Texture icon0 = WarriorIconBundle.LoadAsset<Texture>("Assets/Skill icons Warrior/Icons/Filled/SIW 8.png");
            RPGHeimHudSystem.SkillsBar.UpdateIconImg(0, icon0);

            // unload the icon bundle
            WarriorIconBundle.Unload(false);
            //Assets / Skill icons Warrior/ Icons / Filled / SIW 8.png
        }
    }
}