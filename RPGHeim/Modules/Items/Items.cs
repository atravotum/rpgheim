using BepInEx;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System.Reflection;
using UnityEngine;
using HarmonyLib;

namespace RPGHeim
{
    internal class RPGHeimItemsSystem : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("github.atravotum.rpgheim");

        private void Awake()
        {
            harmony.PatchAll();
        }

        // Load in the class stone used for picking/leveling/skilling for the new class system
        private void CreateClassStone()
        {
            //Load embedded resources and extract gameobject
            AssetBundle classStoneBundle = AssetUtils.LoadAssetBundleFromResources("classstone", Assembly.GetExecutingAssembly());
            GameObject classStonePrefab = classStoneBundle.LoadAsset<GameObject>("Assets/StylRocksMagic/StylRocksMagic_Prefabs/StylRocksMagic_LOD0.prefab");

            // add the piece with jotunn and unload the bundle
            CustomPiece classStone = new CustomPiece(classStonePrefab, new PieceConfig
            {
                PieceTable = "Hammer",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "Stone", Amount = 20 },
                }
            });
            PieceManager.Instance.AddPiece(classStone);
            classStoneBundle.Unload(false);
        }

        // Load in the pieces that can be crafted at the class stone $item_rpgheim_tome_fighter_name
        private void CreateClassStationPieces()
        {
            //Load embedded resources and extract gameobject
            AssetBundle classTomeBundle = AssetUtils.LoadAssetBundleFromResources("classtomes", Assembly.GetExecutingAssembly());
            GameObject fighterTomePrefab = classTomeBundle.LoadAsset<GameObject>("Assets/InnerDriveStudios/FighterTome/Prefab/DruidTome.prefab");

            // create the fighter tome item
            CustomItem fighterTome = new CustomItem(fighterTomePrefab, false, new ItemConfig
            {
                CraftingStation = "StylRocksMagic_LOD0",
                Requirements = new[]
                    {
                        new RequirementConfig { Item = "Wood", Amount = 10 }
                    }
            });
            ItemManager.Instance.AddItem(fighterTome);
            classTomeBundle.Unload(false);
        }

        // handler function for when an RPGHeim item is used to trigger any custom logic
        public static void itemUsed(ItemDrop.ItemData item, Player player)
        {
            Console.print(item.m_shared.m_name);
            if (item.m_shared.m_name == "$item_RPGHeimTomeFighter")
            {
                Skills.SkillDef fighterSkill = SkillManager.Instance.GetSkill("github.atravotum.rpgheim.skills.fighter");
                float currentLevel = player.GetSkillFactor(fighterSkill.m_skill);
                Console.print("Player current level is: " + currentLevel);
                if (currentLevel == 0)
                {
                    player.RaiseSkill(fighterSkill.m_skill, 1);
                }
                else MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, "You already belong to this or another class.");
            }
        }

        // Harmony patch to check when our mod's items are used so we can trigger effects
        [HarmonyPatch(typeof(Player), "ConsumeItem")]
        public static class RPGHeimItemUse
        {
            private static void Postfix(ref Inventory inventory, ref ItemDrop.ItemData item, ref Player __instance)
            {
                if (item.m_shared.m_name.Contains("RPGHeim"))
                {
                    itemUsed(item, __instance);
                }
            }
        }
    }
}