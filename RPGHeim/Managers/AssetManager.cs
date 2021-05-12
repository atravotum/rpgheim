using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using RPGHeim.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace RPGHeim
{
    public static class AssetManager
    {
        public static List<PrefabToLoad<bool>> ProjectilesPrefabs { get; set; } = new List<PrefabToLoad<bool>>();

        public static int ProjectileIndex { get; set; }

        public static IEnumerable<AssetBundleToLoad> AssetBundles = new List<AssetBundleToLoad>()
        {
            new AssetBundleToLoad()
            {
                AssetBundleName = "classstone",
                Pieces = new List<PrefabToLoad<PieceConfig>>()
                {
                    new PrefabToLoad<PieceConfig>()
                    {
                        AssetPath = "Assets/StylRocksMagic/StylRocksMagic_Prefabs/StylRocksMagic_LOD0.prefab",
                        Config = new PieceConfig
                        {
                            PieceTable = "Hammer",
                            Requirements = new[]
                            {
                                new RequirementConfig { Item = "Stone", Amount = 20 },
                                new RequirementConfig { Item = "Flint", Amount = 10 }
                            }
                        },
                    }
                }
            },
            new AssetBundleToLoad()
            {
                AssetBundleName = "classtomes",
                Items = new List<PrefabToLoad<ItemConfig>>()
                {
                    new PrefabToLoad<ItemConfig>()
                    {
                        AssetPath = "Assets/InnerDriveStudios/FighterTome/Prefab/DruidTome.prefab",
                        Config = new ItemConfig
                        {
                            CraftingStation = "StylRocksMagic_LOD0",
                            Requirements = new[]
                            {
                                new RequirementConfig { Item = "Wood", Amount = 10 }
                            }
                        },
                    }
                }
            },
            new AssetBundleToLoad()
            {
                AssetBundleName = "darkprojectile",
                Prefabs = new List<PrefabToLoad<bool>>()
                {
                    // Magic Missile
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/mmprojectile_explosion.prefab",
                        Craftable = false
                    },
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/mmprojectile_projectile.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },

                    // Dark Projectile
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/darkprojectile_explosion.prefab",
                        Craftable = false
                    },
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/darkbolt_projectile.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/fx_dark_projectile_expl.prefab",
                        Craftable = false
                    },

                    // Fireball
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/cfireball_explosion.prefab",
                        Craftable = false
                    },
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/cfireball_projectile.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },

                    // Fireblast
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/fireblast_explosion.prefab",
                        Craftable = false
                    },
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/fireblast_projectile.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },

                    // Magmablast
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/magmablast_explosion.prefab",
                        Craftable = false
                    },
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/magmablast_projectile.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },

                    // Waterball
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/waterball_explosion.prefab",
                        Craftable = false
                    },
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/waterball_projectile.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },

                    // Lightning Blast
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/lightningblast_explosion.prefab",
                        Craftable = false
                    },
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/lightningblast_projectile.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },

                    // Poison Blast
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/poisonblast_explosion.prefab",
                        Craftable = false
                    },
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/poisonblast_projectile.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },

                     // Fire Tornado
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/firetornado_explosion.prefab",
                        Craftable = false
                    },
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/firetornado_projectile.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },

                    // Large Dark Cast
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/darkcast_lg_explosion.prefab",
                        Craftable = false
                    },
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/darkcast_lg_projectile.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },
                    
                    // Ice blast 
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/iceblast_explosion.prefab",
                        Craftable = false
                    },
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/iceblast_projectile.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },

                    // Icewave 
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/icewave_explosion.prefab",
                        Craftable = false
                    },
                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/icewave_projectile.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },

                    //new PrefabToLoad<bool>()
                    //{
                    //    AssetPath = "Assets/CustomItems/DarkProjectile/lightning_shield.prefab",
                    //    Craftable = false,
                    //    IsProjectile = true
                    //},

                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/aura_cleanse.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },

                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/aura_fire.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },

                    new PrefabToLoad<bool>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/aura_cleanse.prefab",
                        Craftable = false,
                        IsProjectile = true
                    },

                    //// Auras --  
                    //new PrefabToLoad<bool>()
                    //{
                    //    AssetPath = "Assets/CustomItems/DarkProjectile/aura_cleanse.prefab",
                    //    Craftable = false
                    //},
                    //new PrefabToLoad<bool>()
                    //{
                    //    AssetPath = "Assets/CustomItems/DarkProjectile/aura_fire.prefab",
                    //    Craftable = false,
                    //    IsProjectile = true
                    //},
                    //new PrefabToLoad<bool>()
                    //{
                    //    AssetPath = "Assets/CustomItems/DarkProjectile/aura_healing.prefab",
                    //    Craftable = false,
                    //    IsProjectile = true
                    //}

                },
                Items = new List<PrefabToLoad<ItemConfig>>()
                {
                    new PrefabToLoad<ItemConfig>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/PriestStaff.prefab",
                        Config = new ItemConfig
                        {
                            Name = "PriestStaff",
                            CraftingStation = "rpgheim_piece_ClassStation",
                            Requirements = new[]
                            {
                                new RequirementConfig { Item = "Wood", Amount = 1 }
                            }
                        }
                    },
                    new PrefabToLoad<ItemConfig>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/TestStaff.prefab",
                        Config = new ItemConfig
                        {
                            Name = "TestStaff",
                            CraftingStation = "rpgheim_piece_ClassStation",
                            Requirements = new[]
                            {
                                new RequirementConfig { Item = "Wood", Amount = 1 }
                            }
                        }
                    },
                    new PrefabToLoad<ItemConfig>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/ShieldTest.prefab",
                        Config = new ItemConfig
                        {
                            Name = "ShieldTest",
                            CraftingStation = "rpgheim_piece_ClassStation",
                            Requirements = new[]
                            {
                                new RequirementConfig { Item = "Wood", Amount = 1 }
                            }
                        }
                    },
                    new PrefabToLoad<ItemConfig>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/ShieldTest2.prefab",
                        Config = new ItemConfig
                        {
                            Name = "ShieldTest2",
                            CraftingStation = "rpgheim_piece_ClassStation",
                            Requirements = new[]
                            {
                                new RequirementConfig { Item = "Wood", Amount = 1 }
                            }
                        }
                    },
                    // Magic Missile Loaded Staff
                    new PrefabToLoad<ItemConfig>()
                    {
                        AssetPath = "Assets/CustomItems/DarkProjectile/MMStaff.prefab",
                        Config = new ItemConfig
                        {
                            Name = "MMStaff",
                            CraftingStation = "rpgheim_piece_ClassStation",
                            Requirements = new[]
                            {
                                new RequirementConfig { Item = "Wood", Amount = 1 }
                            }
                        }
                    }
                }
            }
        };

        public static void RegisterLocalization()
        {
            LocalizationManager.Instance.AddLocalization(new LocalizationConfig("English")
            {
                Translations = {
                    {"item_RPGHeimTomeFighter", "Fighter Class Tome"},
                    {"item_RPGHeimTomeFighter_description", "Unlock your true potential as a skilled figher."},
                    {"piece_RPGHeimClassStone", "Class Stone"},
                    {"piece_RPGHeimClassStone_description", "Gain access to RPGHeim's class items/gameplay."},
                    {"se_RPGHeimFightingSpirit", "Fighting Spirit"},
                    {"se_RPGHeimFightingSpirit_description", "Damage +20%"},
                    {"se_RPGHeimFightingSpirit_tooltip", "Passive Effect: Damage +20%, Carry Weight +75"},
                    {"se_RPGHeimTrainedReflexes", "Trained Reflexes"},
                    {"se_RPGHeimTrainedReflexes_description", "Blocking +50%\nParry +50%"},
                    {"se_RPGHeimTrainedReflexes_tooltip", "Passive Effect: Blocking +50%, Parry +50"},
                    {"se_RPGHeimDualWielding", "Dual Wielding"},
                    {"se_RPGHeimDualWielding_tooltip", "Passive Effect: Can equip specific off-hand type weapons into your off hand. Damage/effects applied from both weapons on attack."},
                    {"se_RPGHeimDualWielding_description", "Can equip specific off-hand type weapons into your off hand. Damage/effects applied from both weapons on attack."},
                    {"se_RPGHeimStrengthWielding", "Strength Wielding"},
                    {"se_RPGHeimStrengthWielding_tooltip", "Passive Effect: Can equip two-handed weapons as one handed weapons"},
                    {"se_RPGHeimStrengthWielding_description", "Can equip two-handed weapons as one handed weapons and still use your off-hand."},
                    {"se_RPGHeimWeaponsMaster", "Weapons Master"},
                    {"se_RPGHeimWeaponsMaster_tooltip", "Passive Effect: All weapon skills are always at Lv. 100"},
                    {"se_RPGHeimWeaponsMaster_description", "Weapon skills always Lv. 100"},
                    {"se_RPGHeimWarCry", "War Cry"},
                    {"se_RPGHeimWarCry_description", "Stamina Regen +50%"},
                    {"se_RPGHeimWarCry_tooltip", "Activation Effect: Stamina Regen +50% to all nearby players for 15 seconds."}
                }
            });
        }

        public static void RegisterPrefabs()
        {
            foreach (var assetBundle in AssetBundles)
            {
                assetBundle.Load();
            }
        }

        /// <summary>
        /// Creates a streamified embedded resource into a byte Array.
        /// </summary>
        /// <param name="input">Name of asset</param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates an Embbedded Resources into a Stream.
        /// </summary>
        /// <param name="assetName">Name of asset.</param>
        /// <returns></returns>
        public static Stream StreamifyEmbeddedResource(string assetName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(assetName);
        }

        public static Texture2D Get2DTexture(string assetName)
        {
            var iconStream = StreamifyEmbeddedResource(assetName);
            var iconByteArray = ReadFully(iconStream);
            Texture2D iconAsTexture = new Texture2D(2, 2);
            iconAsTexture.LoadImage(iconByteArray);
            return iconAsTexture;
        }


        public class SpriteAssets
        {
            public static string FighterIcon { get; set; } = "RPGHeim.AssetsEmbedded.fighterSkillIcon.png";
        }

        // function to get an embeded icon
        private static Dictionary<string, Sprite> _loadedSprites = new Dictionary<string, Sprite>();

        public static Sprite GetResourceSprite (string resourceName)
        {
            if(_loadedSprites.ContainsKey(resourceName))
            {
                return _loadedSprites[resourceName];
            }

            var iconAsTexture = Get2DTexture(resourceName);
            var sprite = Sprite.Create(iconAsTexture, new Rect(0f, 0f, iconAsTexture.width, iconAsTexture.height), Vector2.zero);
            _loadedSprites.Add(resourceName, sprite);
            return sprite;
        }

        public static string LoadText(string path)
        {
            // Just wrapping it to be semi easier..
            return Jotunn.Utils.AssetUtils.LoadText(path);
        }
    }
}
