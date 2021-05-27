using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPGHeim
{
    [HarmonyPatch(typeof(Humanoid), "EquipItem")]
    public static class PlayerItemEquipPatch
    {
        public static void Prefix(ref Humanoid __instance, ref ItemDrop.ItemData item, ref bool triggerEquipEffects)
        {
            if (__instance == Player.m_localPlayer)
            {
                // change item type to shield if dual wielding stat so it can equip to left hand
                StatusEffect dualWieldFlag = __instance.m_seman.GetStatusEffect("SE_DualWielding");
                ItemDrop.ItemData mainWeapon = __instance.GetCurrentWeapon();
                if (dualWieldFlag != null && mainWeapon != null && mainWeapon.m_shared.m_name != "Unarmed")
                    item.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Shield;

                // change two hander if strengh wielding so it can equiped as a one hander
                StatusEffect StrengthWieldFlag = __instance.m_seman.GetStatusEffect("SE_StrengthWielding");
                if (StrengthWieldFlag != null && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
                    item.m_shared.m_itemType = ItemDrop.ItemData.ItemType.OneHandedWeapon;
            }
        }

        public static void Postfix(ref Humanoid __instance, ref ItemDrop.ItemData item, ref bool triggerEquipEffects)
        {
            if (__instance == Player.m_localPlayer)
            {
                // set any  SE multipliers applicable to this patch
                float blockMultiplier = 1f;
                float parryMultiplier = 1f;
                List<StatusEffect> effectList = Player.m_localPlayer.m_seman.GetStatusEffects();
                foreach (var se in effectList.OfType<SE_CustomModifier>())
                {
                    if (!se.m_blockModifier.Equals(null))
                        blockMultiplier += se.m_blockModifier - 1;
                    if (!se.m_parryModifier.Equals(null))
                        parryMultiplier += se.m_parryModifier - 1;
                }

                // apply shield modifers
                if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield)
                {
                    item.m_shared.m_blockPower *= blockMultiplier;
                    item.m_shared.m_deflectionForce *= parryMultiplier;
                }
            }
        }
    }

    [HarmonyPatch(typeof(Humanoid), "UnequipItem")]
    public static class PlayerItemUnEquipPatch
    {
        public static void Postfix(ref Humanoid __instance, ref ItemDrop.ItemData item)
        {
            if (__instance == Player.m_localPlayer && item != null)
            {
                // revert any shield modifiers we may have added
                ItemDrop origItemData = item.m_dropPrefab.GetComponent<ItemDrop>();
                if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield)
                {
                    item.m_shared.m_blockPower = origItemData.m_itemData.m_shared.m_blockPower;
                    item.m_shared.m_deflectionForce = origItemData.m_itemData.m_shared.m_deflectionForce;
                }

                // if we played with the item type for any reason, revert it
                if (item.m_shared.m_itemType != origItemData.m_itemData.m_shared.m_itemType)
                    item.m_shared.m_itemType = origItemData.m_itemData.m_shared.m_itemType;
            }
        }
    }
}
