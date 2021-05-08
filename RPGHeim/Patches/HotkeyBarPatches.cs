using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace RPGHeim
{

    // Token: 0x02000029 RID: 41
    [HarmonyPatch(typeof(HotkeyBar), "UpdateIcons", new Type[] { typeof(Player) })]
    public static class HotkeyBar_UpdateIcons_Patch
    {
        // Token: 0x060000AF RID: 175 RVA: 0x000082B0 File Offset: 0x000064B0
        public static void Postfix(HotkeyBar __instance, List<HotkeyBar.ElementData> ___m_elements, List<ItemDrop.ItemData> ___m_items, Player player)
        {
            return; // If you want to patch  HotKeyBar UdpateIcons (overring the icons for instance).
            //Jotunn.Logger.LogMessage($"Postfixing Hotkeybar - UpdateIcons");
            bool flag = player == null || player.IsDead();
            if (!flag)
            {
                for (int i = 0; i < ___m_elements.Count; i++)
                {
                    HotkeyBar.ElementData elementData = ___m_elements[i];
                    //Image image = ItemBackgroundHelper.CreateAndGetMagicItemBackgroundImage(elementData.m_go, elementData.m_equiped, false);
                    //image.enabled = false;
                }
                for (int j = 0; j < ___m_items.Count; j++)
                {
                    ItemDrop.ItemData itemData = ___m_items[j];
                    HotkeyBar.ElementData elementForItem = HotkeyBar_UpdateIcons_Patch.GetElementForItem(___m_elements, itemData);
                    //bool flag2 = elementForItem == null;
                    //if (flag2)
                    //{
                    //    EpicLoot.LogWarning(string.Format("Tried to get element for {0} at {1}, but element was null (total elements = {2})", itemData.m_shared.m_name, itemData.m_gridPos, ___m_elements.Count));
                    //}
                    //else
                    //{
                    //    Image image2 = ItemBackgroundHelper.CreateAndGetMagicItemBackgroundImage(elementForItem.m_go, elementForItem.m_equiped, false);
                    //    bool flag3 = itemData.UseMagicBackground();
                    //    if (flag3)
                    //    {
                    //        image2.enabled = true;
                    //        image2.sprite = EpicLoot.GetMagicItemBgSprite();
                    //        image2.color = itemData.GetRarityColor();
                    //    }
                    //}
                }

                //Jotunn.Logger.LogMessage($"selected - {__instance.m_selected}");
            }
        }

        // Token: 0x060000B0 RID: 176 RVA: 0x000083D8 File Offset: 0x000065D8
        private static HotkeyBar.ElementData GetElementForItem(List<HotkeyBar.ElementData> elements, ItemDrop.ItemData item)
        {
            int num = (item.m_gridPos.y == 0) ? item.m_gridPos.x : (Player.m_localPlayer.GetInventory().m_width + item.m_gridPos.x - 5);
            return (num >= 0 && num < elements.Count) ? elements[num] : null;
        }
    }
}
