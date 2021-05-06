using Jotunn.Configs;
using Jotunn.Managers;
using RPGHeim.Models;
using System.Collections.Generic;
using System.IO;
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
            /*new AssetBundleToLoad()
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
                    }

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
            }*/
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
                    {"piece_RPGHeimClassStone", "Class Stone"}, {"piece_RPGHeimClassStone_description", "Gain access to RPGHeim's class items/gameplay."},
                    {"item_RPGHeimTomeFighter", "Fighter Class Tome"}, {"item_RPGHeimTomeFighter_description", "Unlock your true potential as a skilled figher."},
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

        public static void RegisterSkills()
        {
            Console.print("ok I am registering the skills yo");
            SkillManager.Instance.AddSkill(new SkillConfig
            {
                Identifier = "github.atravotum.rpgheim.skills.fighter",
                Name = "Fighter",
                Description = "Your current skill as a master of war.",
                Icon = GetResourceSprite("RPGHeim.AssetsEmbedded.fighterSkillIcon.png"),
                IncreaseStep = 1f
            });
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

        // function to get an embeded icon
        public static Sprite GetResourceSprite (string resourceName)
        {
            var iconStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            var iconByteArray = ReadFully(iconStream);
            Texture2D iconAsTexture = new Texture2D(2, 2);
            iconAsTexture.LoadImage(iconByteArray);
            return Sprite.Create(iconAsTexture, new Rect(0f, 0f, iconAsTexture.width, iconAsTexture.height), Vector2.zero);
        }
    }
}
