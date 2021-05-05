using BepInEx.Logging;
using Jotunn.Configs;
using Jotunn.Managers;
using MonoMod.Utils;
using RPGHeim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        LocalizationConfigs = new List<LocalizationConfig>() {
                            new LocalizationConfig("English")
                            {
                                Translations = new Dictionary<string, string>() {
                                    {"piece_RPGHeimClassStone", "Class Stone"},
                                    {"piece_RPGHeimClassStone_description", "Gain access to RPGHeim's class items/gameplay."}
                                }
                            }
                        }
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
                            Name = "rpgheim_item_FighterTome",
                            CraftingStation = "StylRocksMagic_LOD0",
                            Requirements = new[]
                            {
                                new RequirementConfig { Item = "Wood", Amount = 10 }
                            }
                        },
                        LocalizationConfigs = new List<LocalizationConfig>() {
                            new LocalizationConfig("English")
                            {
                                Translations = new Dictionary<string, string>() {
                                    {"item_RPGHeimTomeFighter", "Fighter Class Tome"},
                                    {"item_RPGHeimTomeFighter_description", "Unlock your true potential as a skilled figher."},
                                }
                            }
                        }
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
            }
        };

        public static void RegisterLocalization()
        {
            List<LocalizationConfig> localizationConfigs = new List<LocalizationConfig>();
            foreach (var assetBundle in AssetBundles)
            {
                foreach (var prefab in assetBundle.Pieces)
                {
                    // Grab all the localization configs that exist on peices.
                    localizationConfigs.AddRange(prefab.LocalizationConfigs);
                }

                foreach (var prefab in assetBundle.Items)
                {
                    // Grab all the localization configs that exist on items.
                    localizationConfigs.AddRange(prefab.LocalizationConfigs);
                }

                foreach (var prefab in assetBundle.Prefabs)
                {
                    // Grab all the localization configs that exist on prefabs? Prob none?.
                    localizationConfigs.AddRange(prefab.LocalizationConfigs);
                }
            }

            var localizationByLanguage = new Dictionary<string, LocalizationConfig>();
            // Aggregate all the Localizations by Lauguage
            foreach (var localizationConfig in localizationConfigs)
            {
                if (localizationByLanguage.ContainsKey(localizationConfig.Language)) 
                {
                    // Add it to this langauge.
                    localizationByLanguage[localizationConfig.Language].Translations.AddRange(localizationConfig.Translations);
                }
                else
                {
                    // No laguage is registered yet with this name add it in.
                    localizationByLanguage.Add(localizationConfig.Language, localizationConfig);
                }
            }
            Jotunn.Logger.LogDebug("Loading Localization: ");
            Jotunn.Logger.LogDebug(localizationByLanguage);

            // We have our aggregated terms by language -- Add the localizations configs.
            foreach (var localizationConfig in localizationByLanguage)
            {
                LocalizationManager.Instance.AddLocalization(localizationConfig.Value);
            }
        }

        public static void RegisterPrefabs()
        {
            foreach (var assetBundle in AssetBundles)
            {
                assetBundle.Load();
            }
        }
    }
}
