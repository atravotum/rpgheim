using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace RPGHeim
{
    [HarmonyPatch(typeof(Attack))]
    public static class AttackPatches
    {
        public static AttackPatches.PlayerAttackInfo GetPlayerAttackInfo(Player player)
        {
            AttackPatches.PlayerAttackInfo result;
            AttackPatches.PlayerAttackInfos.TryGetValue(player.GetPlayerID(), out result);
            return result;
        }

        [HarmonyPrefix]
        [HarmonyPatch("FireProjectileBurst")]
        public static void FireProjectileBurstPrefix(Attack __instance)
        {
            if (!__instance.m_character.IsPlayer())
            {
                return;
            }

            Jotunn.Logger.LogMessage($"{__instance.m_weapon.m_shared.m_name} - {AssetManager.ProjectilesPrefabs.Count}");
            if (AssetManager.ProjectilesPrefabs.Count > 0)
            {
                Jotunn.Logger.LogMessage($"{__instance.m_attackProjectile.name} loaded. - Cur index: {AssetManager.ProjectileIndex}");

                var prefabToUse = AssetManager.ProjectilesPrefabs[AssetManager.ProjectileIndex];

                Jotunn.Logger.LogMessage($"{__instance.m_attackProjectile.name} - isnull: {prefabToUse == null}");

                if (prefabToUse == null) 
                    return;

                Jotunn.Logger.LogMessage($"{__instance.m_attackProjectile.name} loading in {prefabToUse.LoadedPrefab.name}");

                if (__instance.m_attackProjectile != prefabToUse.LoadedPrefab)
                {
                    __instance.m_attackProjectile = prefabToUse.LoadedPrefab;
                }
            }

            if (__instance.m_weapon.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Bow)
            {
                return;
            }

            //if (DarkfallMod.UseVanillaBowMechanics.Value)
            //{
            //    return;
            //}
            //__instance.m_projectileAccuracyMin = 20f * (1f - __instance.m_character.GetSkillFactor(Skills.SkillType.Bows));
        }

        // Taken directly from -- Combat one I forget.. sorry.
        //[HarmonyPrefix]
        //[HarmonyPatch("Start")]
        //public static bool StartPrefix(Attack __instance, Humanoid character, Rigidbody body, ZSyncAnimation zanim, CharacterAnimEvent animEvent, VisEquipment visEquipment, ItemDrop.ItemData weapon, Attack previousAttack, float timeSinceLastAttack, float attackDrawPercentage, ref bool __result)
        //{
        //    if (!character.IsPlayer())
        //    {
        //        return true;
        //    }
        //    if (__instance.m_attackAnimation == "")
        //    {
        //        __result = false;
        //        return false;
        //    }
        //    Player player = character as Player;
        //    AttackPatches.PlayerAttackInfo playerAttackInfo = AttackPatches.GetPlayerAttackInfo(player);
        //    if (playerAttackInfo == null)
        //    {
        //        playerAttackInfo = new AttackPatches.PlayerAttackInfo();
        //        AttackPatches.PlayerAttackInfos[player.GetPlayerID()] = playerAttackInfo;
        //    }
        //    __instance.m_character = character;
        //    __instance.m_baseAI = __instance.m_character.GetComponent<BaseAI>();
        //    __instance.m_body = body;
        //    __instance.m_zanim = zanim;
        //    __instance.m_animEvent = animEvent;
        //    __instance.m_visEquipment = visEquipment;
        //    __instance.m_weapon = weapon;
        //    __instance.m_attackDrawPercentage = attackDrawPercentage;
        //    if (Attack.m_attackMask == 0)
        //    {
        //        Attack.m_attackMask = LayerMask.GetMask(new string[]
        //        {
        //            "Default",
        //            "static_solid",
        //            "Default_small",
        //            "piece",
        //            "piece_nonsolid",
        //            "character",
        //            "character_net",
        //            "character_ghost",
        //            "hitbox",
        //            "character_noenv",
        //            "vehicle"
        //        });
        //        Attack.m_attackMaskTerrain = LayerMask.GetMask(new string[]
        //        {
        //            "Default",
        //            "static_solid",
        //            "Default_small",
        //            "piece",
        //            "piece_nonsolid",
        //            "terrain",
        //            "character",
        //            "character_net",
        //            "character_ghost",
        //            "hitbox",
        //            "character_noenv",
        //            "vehicle"
        //        });
        //    }
        //    float staminaUsage = __instance.GetStaminaUsage();
        //    if (staminaUsage > 0f && !character.HaveStamina(staminaUsage + 0.1f))
        //    {
        //        if (character.IsPlayer())
        //        {
        //            Hud.instance.StaminaBarNoStaminaFlash();
        //        }
        //        __instance.m_currentAttackCainLevel = 0;
        //        playerAttackInfo.Reset(null);
        //        __result = false;
        //        return false;
        //    }
        //    if (!Attack.HaveAmmo(character, __instance.m_weapon))
        //    {
        //        __result = false;
        //        return false;
        //    }
        //    Attack.EquipAmmoItem(character, __instance.m_weapon);
        //    if (__instance.m_attackChainLevels > 1)
        //    {
        //        float num = 0.2f;
        //        float num2 = 0f;
        //        switch (weapon.m_shared.m_skillType)
        //        {
        //            case Skills.SkillType.Swords:
        //                num = DarkfallMod.ChainAttackTimeoutSwords.Value;
        //                num2 = DarkfallMod.ConsecutiveHitMultiplierSwords.Value;
        //                break;
        //            case Skills.SkillType.Knives:
        //                num = DarkfallMod.ChainAttackTimeoutKnives.Value;
        //                num2 = DarkfallMod.ConsecutiveHitMultiplierKnives.Value;
        //                break;
        //            case Skills.SkillType.Clubs:
        //                num = DarkfallMod.ChainAttackTimeoutClubs.Value;
        //                num2 = DarkfallMod.ConsecutiveHitMultiplierClubs.Value;
        //                break;
        //            case Skills.SkillType.Polearms:
        //                num = DarkfallMod.ChainAttackTimeoutPolearms.Value;
        //                num2 = DarkfallMod.ConsecutiveHitMultiplierPolearms.Value;
        //                break;
        //            case Skills.SkillType.Spears:
        //                num = DarkfallMod.ChainAttackTimeoutSpears.Value;
        //                num2 = DarkfallMod.ConsecutiveHitMultiplierSpears.Value;
        //                break;
        //            case Skills.SkillType.Axes:
        //                num = DarkfallMod.ChainAttackTimeoutAxes.Value;
        //                num2 = DarkfallMod.ConsecutiveHitMultiplierAxes.Value;
        //                break;
        //            case Skills.SkillType.Unarmed:
        //                num = DarkfallMod.ChainAttackTimeoutUnarmed.Value;
        //                num2 = DarkfallMod.ConsecutiveHitMultiplierUnarmed.Value;
        //                break;
        //            case Skills.SkillType.Pickaxes:
        //                num = DarkfallMod.ChainAttackTimeoutPickaxes.Value;
        //                num2 = DarkfallMod.ConsecutiveHitMultiplierPickaxes.Value;
        //                break;
        //        }
        //        if (previousAttack != null && previousAttack.m_attackAnimation == __instance.m_attackAnimation)
        //        {
        //            __instance.m_currentAttackCainLevel = previousAttack.m_nextAttackChainLevel;
        //        }
        //        if (timeSinceLastAttack > num)
        //        {
        //            __instance.m_currentAttackCainLevel = 0;
        //            playerAttackInfo.Reset(__instance);
        //        }
        //        else if (DarkfallMod.ContinuousChainedAttacks.Value && __instance.m_attackChainLevels > 2)
        //        {
        //            __instance.m_damageMultiplier += (float)playerAttackInfo.ConsecutiveHits * num2;
        //            if (playerAttackInfo.LastAttackHit)
        //            {
        //                playerAttackInfo.ConsecutiveHits++;
        //            }
        //            playerAttackInfo.SetAttack(__instance);
        //            if (__instance.m_currentAttackCainLevel >= __instance.m_attackChainLevels - 1)
        //            {
        //                __instance.m_currentAttackCainLevel = 0;
        //            }
        //        }
        //        __instance.m_zanim.SetTrigger(__instance.m_attackAnimation + __instance.m_currentAttackCainLevel.ToString());
        //    }
        //    else if (__instance.m_attackRandomAnimations >= 2)
        //    {
        //        int num3 = UnityEngine.Random.Range(0, __instance.m_attackRandomAnimations);
        //        __instance.m_zanim.SetTrigger(__instance.m_attackAnimation + num3.ToString());
        //    }
        //    else
        //    {
        //        __instance.m_zanim.SetTrigger(__instance.m_attackAnimation);
        //    }
        //    if (character.IsPlayer() && __instance.m_attackType != Attack.AttackType.None && __instance.m_currentAttackCainLevel == 0)
        //    {
        //        if (ZInput.IsMouseActive() || __instance.m_attackType == Attack.AttackType.Projectile)
        //        {
        //            character.transform.rotation = character.GetLookYaw();
        //            __instance.m_body.rotation = character.transform.rotation;
        //        }
        //        else if (ZInput.IsGamepadActive() && !character.IsBlocking() && character.GetMoveDir().magnitude > 0.3f)
        //        {
        //            character.transform.rotation = Quaternion.LookRotation(character.GetMoveDir());
        //            __instance.m_body.rotation = character.transform.rotation;
        //        }
        //    }
        //    weapon.m_lastAttackTime = Time.time;
        //    __instance.m_animEvent.ResetChain();
        //    playerAttackInfo.LastAttackHit = false;
        //    __result = true;
        //    return false;
        //}

        [HarmonyPrefix]
        [HarmonyPatch("DoMeleeAttack")]
        public static void DoMeleeAttackPrefix(Attack __instance)
        {
            if (!__instance.m_character.IsPlayer())
            {
                return;
            }
            Player player = __instance.m_character as Player;
            AttackPatches.PlayerAttackInfo playerAttackInfo = AttackPatches.PlayerAttackInfos[player.GetPlayerID()];
            if (playerAttackInfo.LastAttack != __instance)
            {
                playerAttackInfo.Reset(null);
                return;
            }
            playerAttackInfo.LastAttackTime = Time.time;
        }

        private static Dictionary<long, AttackPatches.PlayerAttackInfo> PlayerAttackInfos = new Dictionary<long, AttackPatches.PlayerAttackInfo>();

        public class PlayerAttackInfo
        {
            public void SetAttack(Attack newAttack)
            {
                this.LastAttack = newAttack;
                this.LastAttackTime = Time.time;
                this.LastAttackHit = false;
            }

            public void Reset(Attack newAttack)
            {
                this.LastAttack = newAttack;
                this.LastAttackTime = 0f;
                this.LastAttackHit = false;
                this.ConsecutiveHits = 0;
            }

            public Attack LastAttack;
            public float LastAttackTime;
            public bool LastAttackHit;
            public int ConsecutiveHits;
        }
    }
}
