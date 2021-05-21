using Jotunn.Managers;
using System;
using UnityEngine;

namespace RPGHeim
{
    internal class RPGHeimItemsSystem : MonoBehaviour
    {
        // handler function for when an RPGHeim item is used to trigger any custom logic
        public static void itemUsed(ItemDrop.ItemData item, Player player)
        {
            Console.print(item.m_shared.m_name);

            // Class Tome effects
            if (item.m_shared.m_name.Contains("RPGHeimTome"))
            {
                float classLv = 0;
                foreach (string skill in AssetManager.RegisteredSkills)
                {
                    Enum.TryParse(skill, out Skills.SkillType skillType);
                    classLv += player.GetSkillFactor(skillType);
                }

                if (classLv == 0)
                {
                    switch (item.m_shared.m_name)
                    {
                        case "$item_RPGHeimTomeFighter":
                            Enum.TryParse("Fighter", out Skills.SkillType fighterSkill);
                            player.RaiseSkill(fighterSkill, 1);
                        break;

                        case "$item_RPGHeimTomeHealer":
                            Enum.TryParse("Healer", out Skills.SkillType healerSkill);
                            player.RaiseSkill(healerSkill, 1);
                        break;
                    }
                }
                else MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, "You already belong to a class.");
            }
        }
    }
}