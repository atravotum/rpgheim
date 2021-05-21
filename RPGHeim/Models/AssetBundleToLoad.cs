using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using RPGHeim.Managers;
using RPGHeim.Modules.StatusEffects;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Logger = Jotunn.Logger;

namespace RPGHeim.Models
{
    public class AssetBundleToLoad
    {
        public string AssetBundleName { get; set; }
        public bool Loaded { get; set; } = false;

        public List<PrefabToLoad<ItemConfig>> Items = new List<PrefabToLoad<ItemConfig>>();
        public List<PrefabToLoad<PieceConfig>> Pieces = new List<PrefabToLoad<PieceConfig>>();
        public List<PrefabToLoad<bool>> Prefabs = new List<PrefabToLoad<bool>>();

        public List<PrefabToLoad<RuntimeAnimatorController>> AnimationOverrideControllers = new List<PrefabToLoad<RuntimeAnimatorController>>();

        public void Load()
        {
            try
            {
                //Load embedded resources and extract gameobject(s)
                var assetBundle = AssetUtils.LoadAssetBundleFromResources(AssetBundleName, Assembly.GetExecutingAssembly());
                foreach (var prefabToLoad in Pieces)
                {
                    try
                    {
                        prefabToLoad.LoadedPrefab = assetBundle.LoadAsset<GameObject>(prefabToLoad.AssetPath);

                        // add the piece with jotunn and unload the bundle
                        var customPiece = new CustomPiece(prefabToLoad.LoadedPrefab, prefabToLoad.Config);
                        PieceManager.Instance.AddPiece(customPiece);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Failed to Load {prefabToLoad.AssetPath}: {ex}");
                    }
                }

                foreach (var prefabToLoad in Items)
                {
                    try
                    {
                        prefabToLoad.LoadedPrefab = assetBundle.LoadAsset<GameObject>(prefabToLoad.AssetPath);

                        var itemDropScript = prefabToLoad.LoadedPrefab.GetComponent<ItemDrop>();
                        if(itemDropScript != null)
                        {
                            var skill = RPGHeim.SkillsManager.GetSkill(prefabToLoad.Skill).m_skill;
                            itemDropScript.m_itemData.m_shared.m_skillType = skill;
                            Debug.Log("Setting custom skill to custom item");
                        }
                        // add the piece with jotunn and unload the bundle
                        var customItem = new CustomItem(prefabToLoad.LoadedPrefab, false, prefabToLoad.Config);
                        ItemManager.Instance.AddItem(customItem);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Failed to Load {prefabToLoad.Config.Name}: {ex}");
                    }
                }

                foreach (var prefabToLoad in AnimationOverrideControllers)
                {
                    try
                    {
                        prefabToLoad.LoadedPrefab = assetBundle.LoadAsset<GameObject>(prefabToLoad.AssetPath);
                        if(prefabToLoad.LoadedPrefab != null)
                        {
                            var animator = prefabToLoad.LoadedPrefab.GetComponent<Animator>();
                            prefabToLoad.Config = animator.runtimeAnimatorController;
                            PrefabManager.Instance.AddPrefab(prefabToLoad.LoadedPrefab);
                            Logger.LogMessage("Got Animator Override Controller");
                        }
                        else
                        {
                            Logger.LogMessage("Failed to get animator.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Failed to Load {prefabToLoad.AssetPath}: {ex}");
                    }
                }

                foreach (var prefabToLoad in Prefabs)
                {
                    try
                    {  
                        prefabToLoad.LoadedPrefab = assetBundle.LoadAsset<GameObject>(prefabToLoad.AssetPath);
                        PrefabManager.Instance.AddPrefab(prefabToLoad.LoadedPrefab);
                        // Handles if it needs to.
                        ProjectileManager.Register(prefabToLoad);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Failed to Load {prefabToLoad.AssetPath}: {ex}");
                    }
                }

                assetBundle.Unload(false);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }
    }
}
