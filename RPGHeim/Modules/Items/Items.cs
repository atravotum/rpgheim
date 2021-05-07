using Jotunn.Managers;
using UnityEngine;
using HarmonyLib;

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
                Skills.SkillDef fighterSkill = SkillManager.Instance.GetSkill("github.atravotum.rpgheim.skills.fighter");
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

    // Harmony patch to check when our mod's items are used so we can trigger effects
    [HarmonyPatch(typeof(Player), "ConsumeItem")]
    public static class RPGHeim_Player_ConsumeItem_Patch
    {
        private static void Postfix(ref Inventory inventory, ref ItemDrop.ItemData item, ref Player __instance)
        {
            if (item.m_shared.m_name.Contains("RPGHeim"))
            {
                RPGHeimItemsSystem.itemUsed(item, __instance);
            }
        }
    }
}