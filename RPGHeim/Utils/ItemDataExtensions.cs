using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeim
{
    public static class ItemDataExtensions
    {
        // Just an example of something we could do (making items class specific)
        // Found the extended item data from another mod - useful useage example?
        public static bool IsRPGHeimItem(this ItemDrop.ItemData itemData)
        {
            return itemData.m_shared.m_name.Contains("RPGHeim");
            //ExtendedItemData extendedItemData = ItemDataExtensions.Extended(itemData);
            //return ((extendedItemData != null) ? extendedItemData.GetComponent<MagicItemComponent>() : null) != null;
        }
    }
}
