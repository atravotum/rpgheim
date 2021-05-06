using Jotunn.Managers;
using UnityEngine;

namespace RPGHeim
{
    internal class RPGHeimItemsSystem : MonoBehaviour
    {
        // handler function for when an RPGHeim item is used to trigger any custom logic
        public static void itemUsed(ItemDrop.ItemData item, Player player)
        {
            Console.print("Ok I'm here in the method...");
            Console.print(item.m_shared.m_name);
            if (item.m_shared.m_name == "$item_RPGHeimTomeFighter")
            {
                Console.print("Yep the item was the fighter tome yo");
                Skills.SkillDef fighterSkill = SkillManager.Instance.GetSkill("github.atravotum.rpgheim.skills.fighter");
                Console.print("Ok found the skill it is: " + fighterSkill);
                float currentLevel = player.GetSkillFactor(fighterSkill.m_skill);
                Console.print("Player current level is: " + currentLevel);
                if (currentLevel == 0)
                {
                    Console.print("Current level was lower than 0 so we going to raise this mutha");
                    player.RaiseSkill(fighterSkill.m_skill, 1);
                }
                else MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, "You already belong to this or another class.");
            }
        }
    }
}