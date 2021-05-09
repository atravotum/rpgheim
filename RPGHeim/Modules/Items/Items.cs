using UnityEngine;

namespace RPGHeim
{
    internal class RPGHeimItemsSystem : MonoBehaviour
    {
        // handler function for when an RPGHeim item is used to trigger any custom logic
        public static void itemUsed(ItemDrop.ItemData item, Player player)
        {
            Console.print(item.m_shared.m_name);
            if (item.m_shared.m_name == "$item_RPGHeimTomeFighter")
            {
                Skills.SkillDef fighterSkill = RPGHeim.SkillsManager.GetSkill(SkillsManager.RPGHeimSkill.Fighter);
                float currentLevel = player.GetSkillFactor(fighterSkill.m_skill);
                if (currentLevel == 0)
                {
                    player.RaiseSkill(fighterSkill.m_skill, 1);
                    RPGHeimFighterClass.InitializePlayer(player, 1);
                }
                else MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, "You already belong to this/another class.");
            }
        }
    }
}