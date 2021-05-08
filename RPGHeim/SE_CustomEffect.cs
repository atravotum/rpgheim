using HarmonyLib;
using Jotunn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeim
{
    public class SE_CustomEffect : SE_Stats
    {
        public SE_CustomEffect()
        {
            name = "SE_CustomEffect";
            m_icon = AssetManager.GetResourceSprite(AssetManager.SpriteAssets.FighterIcon);
            m_tooltip = "";
            m_name = "Test Buff";
            m_addMaxCarryWeight = 600;
        }

        [HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyMaxCarryWeight))]
        public static class AddCarryWeight_SEMan_ModifyMaxCarryWeight_Patch
        {
            public static void Postfix(SEMan __instance, ref float limit)
            {
                foreach (var buff in RPGHeimMain.StatusEffects)
                {
                    var hasBuff = (SE_Stats)__instance.GetStatusEffect(buff.name);
                    if (hasBuff != null)
                    {
                        // Add in carry weight if it exists?
                        limit += hasBuff.m_addMaxCarryWeight;
                    }
                }
            }
        }
    }

}
