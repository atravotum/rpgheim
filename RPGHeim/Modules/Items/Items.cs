using System.Collections.Generic;
using UnityEngine;

namespace RPGHeim
{
    internal class RPGHeimItemsSystem : MonoBehaviour
    {
        // handler function for when an RPGHeim item is used to trigger any custom logic
        public static void itemUsed(ItemDrop.ItemData item, Player player)
        {
            /*Console.print(item.m_shared.m_name);
            if (item.m_shared.m_name.Contains("RPGHeimTome"))
            {
                float classLv = 0;
                foreach (SkillsManager.RPGHeimSkill skillEnum in SkillsManager.RPGHeimSkill.GetValues(typeof(SkillsManager.RPGHeimSkill)))
                {
                    Skills.SkillType classSkill = SkillsManager.GetSkill(skillEnum).m_skill;
                    classLv += player.GetSkillFactor(classSkill);
                }

                if (classLv == 0)
                {
                    switch (item.m_shared.m_name)
                    {
                        case "$item_RPGHeimTomeFighter":
                            player.RaiseSkill(
                                SkillsManager.GetSkill(SkillsManager.RPGHeimSkill.Fighter).m_skill,
                                1
                            );
                            RPGHeimFighterClass.InitializePlayer(player, 1);
                        break;

                        case "$item_RPGHeimTomeHealer":
                            player.RaiseSkill(
                                SkillsManager.GetSkill(SkillsManager.RPGHeimSkill.Healer).m_skill,
                                1
                            );
                            RPGHeimHealerClass.InitializePlayer(player, 1);
                        break;
                    }
                }
                else MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, "You already belong to a class.");
            }*/
        }
    }
}