using Jotunn.Configs;
using Jotunn.Managers;
using RPGHeim.Models;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using Jotunn.Utils;
using System;
using Jotunn.Entities;

namespace RPGHeim
{
    public static class AssetManager
    {
        private static AssetBundle IconBundle;
        private static AssetBundle PrefabBundle;
        private static Dictionary<string, Ability> RegisteredAbilities = new Dictionary<string, Ability>();
        private static Dictionary<string, string> EnglishTranslations = new Dictionary<string, string>();
        
        private static string ReadEmbeddedJSON(string ResourceName)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName);
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        public static void LoadAssets ()
        {
            // load bundles
            IconBundle = AssetUtils.LoadAssetBundleFromResources("icons", Assembly.GetExecutingAssembly());
            PrefabBundle = AssetUtils.LoadAssetBundleFromResources("prefabs", Assembly.GetExecutingAssembly());

            // run registration methods
            RegisterStatusEffects();

            // get a list of all the embedded resource files and loop over them and create assets as needed
            string[] EmbeddedResources = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach(string ResourceName in EmbeddedResources)
            {
                if (ResourceName.Contains("Prefab_"))
                    RegisterPrefab(ResourceName);

                if (ResourceName.Contains("Ability_"))
                    RegisterAbility(ResourceName);

                if (ResourceName.Contains("Skill_"))
                    RegisterSkill(ResourceName);
            }

            // unload our loaded bundles
            IconBundle.Unload(false);
            PrefabBundle.Unload(false);

            // register the translations
            RegisterTranslations();
        }
        private static void RegisterStatusEffects()
        {
            try
            {
                SE_Cleanse SECleanse = ScriptableObject.CreateInstance<SE_Cleanse>();
                SECleanse.name = "SE_Cleanse";
                ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(SECleanse, fixReference: false));

                SE_CustomModifier SECustomModifier= ScriptableObject.CreateInstance<SE_CustomModifier>();
                SECustomModifier.name = "SE_CustomModifier";
                ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(SECustomModifier, fixReference: false));
            }
            catch (Exception err)
            {
                Console.print("Unable to register status effects: " + err);
            }
        }
        private static void RegisterAbility(string ResourceName)
        {
            try
            {
                // read the json and parse it into it's respective json class
                string jsonData = ReadEmbeddedJSON(ResourceName);
                Models.JSON.JSON_Ability obj = SimpleJson.SimpleJson.DeserializeObject<Models.JSON.JSON_Ability>(jsonData);
                Ability newAbility = new Ability();

                // convert any enum values
                Enum.TryParse(obj.Type, out Ability.AbilityType abilityType);

                // set the basic ability properties
                newAbility.Name = obj.Name;
                newAbility.Tooltip = obj.Tooltip;
                newAbility.Type = abilityType;
                newAbility.Skill = obj.Name;
                newAbility.CooldownMax = obj.CooldownMax != null ? float.Parse(obj.CooldownMax) : 0;
                newAbility.StaminaCost = obj.StaminaCost != null ? float.Parse(obj.StaminaCost) : 0;
                newAbility.Icon = IconBundle.LoadAsset<Texture>(obj.Icon);

                // add the effects to the ability
                foreach (Models.JSON.JSON_Ability.Effect effect in obj.Effects)
                {
                    Ability.Effect newEffect = new Ability.Effect();

                    // convert any enum values
                    Enum.TryParse(effect.Target, out Ability.Effect.TargetType targetType);
                    Enum.TryParse(effect.DamageType, out HitData.DamageType damageType);

                    // set the basic effect properties
                    newEffect.Target = targetType;
                    newEffect.DamageValue = effect.DamageValue != null ? float.Parse(effect.DamageValue) : 0;
                    newEffect.HealValue = effect.HealValue != null ? float.Parse(effect.HealValue) : 0;
                    if (effect.DamageType != null && !damageType.Equals(null)) newEffect.DamageType = damageType;

                    // add the status effects to this effect
                    foreach (Ability.Effect.StatusEffect statusEffect in effect.StatusEffects)
                    {
                        newEffect.StatusEffects.Add(statusEffect);
                    }

                    // add the new effect to our new ability
                    newAbility.Effects.Add(newEffect);
                }

                // add the translations to our system list
                foreach (KeyValuePair<string, string> entry in obj.Translations)
                {
                    EnglishTranslations.Add(entry.Key, entry.Value);
                }

                // add the new ability to our system list
                RegisteredAbilities.Add(newAbility.Name, newAbility);
            }
            catch (Exception err)
            {
                Console.print("Unable to register ability: " + ResourceName + " => " + err);
            }
        }
        public static Ability GetAbility(string abilityName)
        {
            Ability AbilityResult;
            RegisteredAbilities.TryGetValue(abilityName, out AbilityResult);
            return AbilityResult;
        }
        private static void RegisterSkill(string ResourceName)
        {
            try
            {
                // read the json and parse it into it's respective json class
                string jsonData = ReadEmbeddedJSON(ResourceName);
                Models.JSON.JSON_Skill obj = SimpleJson.SimpleJson.DeserializeObject<Models.JSON.JSON_Skill>(jsonData);
                SkillConfig newSkill = new SkillConfig();

                // get the icon
                Texture2D iconTexture = (Texture2D)IconBundle.LoadAsset<Texture>(obj.Icon);

                // set the basic properties
                newSkill.Name = obj.Name;
                newSkill.Identifier = obj.Identifier;
                newSkill.Description = obj.Description;
                newSkill.IncreaseStep = float.Parse(obj.IncreaseStep);
                newSkill.Icon = Sprite.Create(iconTexture, new Rect(0f, 0f, iconTexture.width, iconTexture.height), Vector2.zero);

                // add the translations to our system list
                foreach (KeyValuePair<string, string> entry in obj.Translations)
                {
                    EnglishTranslations.Add(entry.Key, entry.Value);
                }

                // add the new obj through Jotunn
                SkillManager.Instance.AddSkill(newSkill);
            }
            catch (Exception err)
            {
                Console.print("Unable to register skill: " + ResourceName + " => " + err);
            }
        }
        private static void RegisterPrefab(string ResourceName)
        {
            try
            {
                // read the json and parse it into it's respective json class
                string jsonData = ReadEmbeddedJSON(ResourceName);
                Models.JSON.JSON_Prefab obj = SimpleJson.SimpleJson.DeserializeObject<Models.JSON.JSON_Prefab>(jsonData);
                GameObject prefab = PrefabBundle.LoadAsset<GameObject>(obj.Prefab);

                switch (obj.Type)
                {
                    case "Piece":
                        // create the piece config and prep it
                        var pieceConfig = new PieceConfig();
                        pieceConfig.PieceTable = obj.PieceConfig.PieceTable;

                        // Add the ingredient requirements to the recipe
                        var requirements = new List<RequirementConfig>();
                        foreach (KeyValuePair<string, int> requirement in obj.PieceConfig.Requirements)
                        {
                            var newRequirement = new RequirementConfig();
                            newRequirement.Item = requirement.Key;
                            newRequirement.Amount = requirement.Value;
                            requirements.Add(newRequirement);
                        }
                        pieceConfig.Requirements = requirements.ToArray();

                        // init the piece and add it to the game with jotunn
                        CustomPiece newPiece = new CustomPiece(prefab, pieceConfig);
                        PieceManager.Instance.AddPiece(newPiece);
                   break;

                    case "Item":
                        // init the item and add it to the game with jotunn
                        CustomItem newItem = new CustomItem(prefab, false);
                        ItemManager.Instance.AddItem(newItem);

                        // create the item recipe and prep some details
                        Recipe newRecipe = ScriptableObject.CreateInstance<Recipe>();
                        newRecipe.name = "Recipe_" + newItem.ItemDrop.name;
                        newRecipe.m_item = prefab.GetComponent<ItemDrop>();
                        newRecipe.m_craftingStation = Mock<CraftingStation>.Create(obj.RecipeConfig.CraftingStation);

                        // Add the ingredient requirements to the recipe
                        var ingredients = new List<Piece.Requirement>();
                        foreach(KeyValuePair<string, int> ingredient in obj.RecipeConfig.Ingredients)
                        {
                            ingredients.Add(MockRequirement.Create(ingredient.Key, ingredient.Value));
                        }
                        newRecipe.m_resources = ingredients.ToArray();

                        // add the custom recipe to the game with jotunn
                        CustomRecipe customRecipe = new CustomRecipe(newRecipe, true, true);
                        ItemManager.Instance.AddRecipe(customRecipe);
                    break;
                }
            }
            catch (Exception err)
            {
                Console.print("Unable to register prefab: " + ResourceName + " => " + err);
            }
        }
        private static void RegisterTranslations()
        {
            try
            {
                LocalizationManager.Instance.AddLocalization(new LocalizationConfig("English")
                {
                    Translations = EnglishTranslations
                });
            }
            catch (Exception err)
            {
                Console.print("Unable to register translations: " + err);
            }
        }
    }
}
