using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace RPGHeim
{
    [HarmonyPatch(typeof(HitData), "GetTotalBlockableDamage")]
    public static class ApplyBlockingModifiers
    {
        public static void Postfix(HitData __instance, ref float __result)
        {
            List<StatusEffect> effectList = Player.m_localPlayer.m_seman.GetStatusEffects();
            float multiplier = 1f;
            Console.print("CurrentBlock is: " + __result);
            foreach (var se in effectList.OfType<SE_CustomModifier>())
            {
                Console.print(se.name);
                Console.print("Block Modifier is: " + se.m_blockModifier);
                if (!se.m_blockModifier.Equals(null))
                    multiplier += se.m_blockModifier - 1;
            }
            Console.print("Multiplier is: " + multiplier);
            Console.print("New Block will be: " + __result * multiplier);
            __result *= multiplier;
        }
    }

    [HarmonyPatch(typeof(ItemDrop.ItemData), "GetDeflectionForce", new Type[] { typeof(int) })]
    public static class ModifyParry_ItemData_GetDeflectionForce_Patch
    {
        public static void Postfix(ItemDrop.ItemData __instance, ref float __result)
        {
            List<StatusEffect> effectList = Player.m_localPlayer.m_seman.GetStatusEffects();
            float multiplier = 1f;
            Console.print("CurrentParry is: " + __result);
            foreach (var se in effectList.OfType<SE_CustomModifier>())
            {
                Console.print(se.name);
                Console.print(se.m_parryModifier);
                if (!se.m_parryModifier.Equals(null))
                    multiplier += se.m_parryModifier - 1;
            }
            Console.print("New parry will be: " + __result * multiplier);
            __result *= multiplier;
        }
    }
}
