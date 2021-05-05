using BepInEx;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using Logger = Jotunn.Logger;
using Object = UnityEngine.Object;
using System.Collections.Generic;

namespace RPGHeim
{
    [BepInPlugin("github.atravotum.rpgheim", "RPGHeim", "1.0.0")]
    [BepInDependency(Jotunn.Main.ModGuid)]

    internal class RPGHeimMain : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("github.atravotum.rpgheim");

        private void Awake()
        {
            AssetManager.RegisterPrefabs();
            AssetManager.RegisterLocalization();
            AddNewSkills();
            harmony.PatchAll();
        }

        /// <summary>
        /// Game tick updates - Check for custom inputs
        /// </summary>
        private void Update()
        {
            // Since our Update function in our BepInEx mod class will load BEFORE Valheim loads,
            // we need to check that ZInput is ready to use first.
            if (ZInput.instance != null)
            {
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha1))
                {
                    AssetManager.ProjectileIndex++;
                    if (AssetManager.ProjectileIndex >= AssetManager.ProjectilesPrefabs.Count)
                    {
                        AssetManager.ProjectileIndex = 0;
                    }
                }
            }
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

        // load up our new skills
        private void AddNewSkills()
        {
            // create the fighter skill
            var iconStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RPGHeim.AssetsEmbedded.fighterSkillIcon.png");
            var iconByteArray = ReadFully(iconStream);
            Texture2D iconAsTexture = new Texture2D(2, 2);
            iconAsTexture.LoadImage(iconByteArray);
            Sprite iconAsSprite = Sprite.Create(iconAsTexture, new Rect(0f, 0f, iconAsTexture.width, iconAsTexture.height), Vector2.zero);
            SkillManager.Instance.AddSkill(new SkillConfig
            {
                Identifier = "github.atravotum.rpgheim.skills.fighter",
                Name = "Fighter",
                Description = "Your current level as a fighter.",
                Icon = iconAsSprite,
                IncreaseStep = 1f
            });
        }

        // Adds localizations with configs
        private void AddLocalizations()
        {
            // Add static translations *todo later change this to be imported
            LocalizationManager.Instance.AddLocalization(new LocalizationConfig("English")
            {
                Translations = {
                    {"piece_RPGHeimClassStone", "Class Stone"}, {"piece_RPGHeimClassStone_description", "Gain access to RPGHeim's class items/gameplay."},
                    {"item_RPGHeimTomeFighter", "Fighter Class Tome"}, {"item_RPGHeimTomeFighter_description", "Unlock your true potential as a skilled figher."},
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

        // function to handle use of an RPGHeim item
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

        // harmony patch to add a new hotkeybar to the players screen
        [HarmonyPatch(typeof(Hud), "Awake")]
        public static class Hud_Awake_Patch
        {
            public static void Postfix(Hud __instance)
            {
                HotkeyBar hotKeyBarChildComponent = __instance.GetComponentInChildren<HotkeyBar>();
                if (hotKeyBarChildComponent.transform.parent.Find("RPGHeimQuickCastBar") == null)
                {
                    GameObject newHotKeyBar = Object.Instantiate(hotKeyBarChildComponent.gameObject, __instance.m_healthBarRoot, worldPositionStays: true);
                    newHotKeyBar.name = "hotKeyBarChildComponent";
                    (newHotKeyBar.transform as RectTransform).anchoredPosition = new Vector2(55f, -500f);
                    newHotKeyBar.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
                    newHotKeyBar.GetComponent<HotkeyBar>().m_selected = -1;

                    foreach (HotkeyBar.ElementData element2 in newHotKeyBar.GetComponent<HotkeyBar>().m_elements)
                    {
                        Console.print("kk should do it now");
                        if (element2 != null) Console.print(element2);
                        Object.Destroy(element2.m_go);
                    }
                }
            }
        }
    }
}