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
                foreach (string skillIdentifier in AssetManager.RegisteredSkills)
                {
                    var skillDef = SkillManager.Instance.GetSkill(skillIdentifier);
                    classLv += player.GetSkillFactor(skillDef.m_skill);
                }

                if (classLv == 0)
                {
                    switch (item.m_shared.m_name)
                    {
                        case "$item_RPGHeimTomeFighter":
                            var fighterSkill = SkillManager.Instance.GetSkill("skills.rpgheim.class.fighter");
                            player.RaiseSkill(fighterSkill.m_skill, 1);
                        break;

                        case "$item_RPGHeimTomeHealer":
                            var healerSkill = SkillManager.Instance.GetSkill("skills.rpgheim.class.healer");
                            player.RaiseSkill(healerSkill.m_skill, 1);
                        break;

                        case "$item_RPGHeimTomeRogue":
                            var rogueSkill = SkillManager.Instance.GetSkill("skills.rpgheim.class.rogue");
                            player.RaiseSkill(rogueSkill.m_skill, 1);
                        break;

                        case "$item_RPGHeimTomeWizard":
                            var wizardSkill = SkillManager.Instance.GetSkill("skills.rpgheim.class.wizard");
                            player.RaiseSkill(wizardSkill.m_skill, 1);
                        break;
                    }
                }
                else MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, "You already belong to a class.");
            }
        }
    }
}