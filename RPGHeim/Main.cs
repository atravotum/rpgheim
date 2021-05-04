using BepInEx;
using BepInEx.Configuration;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using Logger = Jotunn.Logger;

namespace RPGHeim
{
    [BepInPlugin("github.atravotum.rpgheim", "RPGHeim", "1.0.0")]
    [BepInDependency(Jotunn.Main.ModGuid)]

    internal class RPGHeimMain : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("github.atravotum.rpgheim");

        private void Awake()
        {
            AddLocalizations();
            CreateClassStone();
            CreateClassStationPieces();
            harmony.PatchAll();
        }

        // function for converting a read embedded resource stream into an 8bit array or something like that
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        // Adds localizations with configs
        private void AddLocalizations()
        {
            // Add static translations *todo later change this to be imported
            LocalizationManager.Instance.AddLocalization(new LocalizationConfig("English")
            {
                Translations = {
                    {"piece_RPGHeimClassStone", "Class Stone"}, {"piece_RPGHeimClassStone_description", "Gain access to RPGHeim's class items/gameplay."},
                    {"item_RPGHeimTomeFighter", "Fighter Class Tome"}, {"item_RPGHeimTomeFighter_description", "Unlock your true potential as a skill figher."},
                }
            });
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
                Name = "rpgheim_piece_ClassStation",
                PieceTable = "Hammer",
                Requirements = new[]
                {
                    new RequirementConfig { Item = "Stone", Amount = 20 },
                    new RequirementConfig { Item = "Flint", Amount = 10 }
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
                Name = "rpgheim_item_FighterTome",
                CraftingStation = "rpgheim_piece_ClassStation",
                Requirements = new[]
                    {
                        new RequirementConfig { Item = "Wood", Amount = 10 }
                    }
            });
            ItemManager.Instance.AddItem(fighterTome);

            // unload the resource bundle
            classTomeBundle.Unload(false);
        }

        // function to handle use of an RPGHeim item
        public static void itemUsed(ItemDrop.ItemData item)
        {
           
        }

        // Harmony patch to check when our mod's items are used so we can trigger effects
        [HarmonyPatch(typeof(Player), "ConsumeItem")]
        public static class RPGHeimItemUse
        {
            private static void Postfix(ref Inventory inventory, ref ItemDrop.ItemData item, ref Player __instance)
            {
                Console.print("Player was: " + __instance);
                if (item.m_shared.m_name.Contains("rpgheim"))
                {
                    itemUsed(item);
                }
            }
        }

        
    }
}