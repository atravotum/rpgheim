using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace RPGHeim
{
    [HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyMaxCarryWeight))]
    public static class AddCarryWeight_SEMan_ModifyMaxCarryWeight_Patch
    {
        public static void Postfix(SEMan __instance, ref float limit)
        {
            SE_Stats fighterBuff = (SE_Stats)Player.m_localPlayer.m_seman.GetStatusEffect("SE_FightingSpirit");
            if (fighterBuff) limit += fighterBuff.m_addMaxCarryWeight;
        }
    }
}
