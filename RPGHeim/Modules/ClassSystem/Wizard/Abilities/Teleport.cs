using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGHeim.Modules.ClassSystem.Wizard.Abilities
{
    public class Teleport
    {
        public static void Execute(Player player, ref float altitude)
        {
            UnityEngine.Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("sfx_perfectblock"), player.transform.position, Quaternion.identity);
            UnityEngine.Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("vfx_stonegolem_attack_hit"), player.transform.position, Quaternion.identity);
            float level = RPGHeim.SkillsManager.GetRPGHeimSkillFactor(player, SkillsManager.RPGHeimSkill.Wizard);
            float num = 0.8f + level * 0.005f * 1;
            //bool flag = player.GetSEMan().HaveStatusEffect("SE_VL_Berserk") || player.GetSEMan().HaveStatusEffect("SE_VL_Execute");
            //if (flag)
            //{
            //	SE_Berserk se_Berserk = (SE_Berserk)player.GetSEMan().GetStatusEffect("SE_VL_Berserk");
            //	bool flag2 = se_Berserk != null;
            //	if (flag2)
            //	{
            //		num *= se_Berserk.damageModifier;
            //	}
            //}
            Vector3 lookDir = player.GetLookDir();
            lookDir.y = 0f;
            player.transform.rotation = Quaternion.LookRotation(lookDir);
            Vector3 rayCastHitPoint = default(Vector3);
            Vector3 currentForward = player.transform.forward;
            Vector3 currentPosition = player.transform.position;
            Vector3 modifiedCurrentPosition = player.transform.position;
            modifiedCurrentPosition.y += 0.1f;
            List<int> list = new List<int>();
            int i;
            for (i = 0; i <= 10; i++)
            {
                RaycastHit raycastHit = default(RaycastHit);
                bool flag3 = false;
                for (int j = 0; j <= 10; j++)
                {
                    Vector3 vector4 = Vector3.MoveTowards(player.transform.position, player.transform.position + currentForward * 100, (float)i + (float)j * 0.1f);
                    vector4.y = modifiedCurrentPosition.y;
                    bool flag4 = vector4.y < ZoneSystem.instance.GetGroundHeight(vector4);
                    if (flag4)
                    {
                        modifiedCurrentPosition.y = ZoneSystem.instance.GetGroundHeight(vector4) + 1f;
                        vector4.y = modifiedCurrentPosition.y;
                    }
                    flag3 = Physics.SphereCast(vector4, 0.05f, currentForward, out raycastHit, float.PositiveInfinity, Script_Layermask);
                    bool flag5 = flag3 && raycastHit.collider;
                    if (flag5)
                    {
                        rayCastHitPoint = raycastHit.point;
                        break;
                    }
                }
                currentPosition = Vector3.MoveTowards(player.transform.position, player.transform.position + currentForward * 100f, (float)i);
                currentPosition.y = ((ZoneSystem.instance.GetSolidHeight(currentPosition) - ZoneSystem.instance.GetGroundHeight(currentPosition) <= 1f) ? ZoneSystem.instance.GetSolidHeight(currentPosition) : ZoneSystem.instance.GetGroundHeight(currentPosition));
                bool flag6 = flag3 && Vector3.Distance(new Vector3(currentPosition.x, modifiedCurrentPosition.y, currentPosition.z), rayCastHitPoint) <= 1f;
                if (flag6)
                {
                    modifiedCurrentPosition = Vector3.MoveTowards(rayCastHitPoint, modifiedCurrentPosition, 1f);
                    UnityEngine.Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("vfx_beehive_hit"), modifiedCurrentPosition, Quaternion.identity);
                    break;
                }
                UnityEngine.Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("vfx_beehive_hit"), modifiedCurrentPosition, Quaternion.identity);
                modifiedCurrentPosition = new Vector3(currentPosition.x, modifiedCurrentPosition.y, currentPosition.z);
                foreach (Character character in Character.GetAllCharacters())
                {
                    HitData hitData = new HitData();
                    hitData.m_damage = player.GetCurrentWeapon().GetDamage();
                    hitData.ApplyModifier(UnityEngine.Random.Range(0.8f, 1.2f) * num);
                    hitData.m_point = character.GetCenterPoint();
                    hitData.m_dir = character.transform.position - currentPosition;
                    hitData.m_skill = RPGHeim.SkillsManager.GetSkill(SkillsManager.RPGHeimSkill.Wizard).m_skill;
                    float num2 = Vector3.Distance(character.transform.position, currentPosition);
                    bool flag7 = !character.IsPlayer() && num2 <= 3f && !list.Contains(character.GetInstanceID());
                    //if (flag7)
                    //{
                    //	SE_Execute se_Execute = (SE_Execute)player.GetSEMan().GetStatusEffect("SE_VL_Execute");
                    //	bool flag8 = se_Execute != null;
                    //	if (flag8)
                    //	{
                    //		hitData.ApplyModifier(se_Execute.damageBonus);
                    //		se_Execute.hitCount--;
                    //		bool flag9 = se_Execute.hitCount <= 0;
                    //		if (flag9)
                    //		{
                    //			player.GetSEMan().RemoveStatusEffect(se_Execute, false);
                    //		}
                    //	}
                    //	character.ApplyDamage(hitData, true, true, HitData.DamageModifier.Normal);
                    //	UnityEngine.Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("fx_crit"), character.GetCenterPoint(), Quaternion.identity);
                    //	list.Add(character.GetInstanceID());
                    //}
                }
            }
            list.Clear();
            bool flag10 = i > 10 && ZoneSystem.instance.GetSolidHeight(modifiedCurrentPosition) - modifiedCurrentPosition.y <= 2f;
            if (flag10)
            {
                modifiedCurrentPosition.y = ZoneSystem.instance.GetSolidHeight(modifiedCurrentPosition);
            }
            player.transform.position = modifiedCurrentPosition;
            altitude = 0f;
            player.transform.rotation = Quaternion.LookRotation(currentForward);
        }


        private static int Script_Layermask = LayerMask.GetMask(new string[]
            {
            "Default",
            "static_solid",
            "Default_small",
            "piece_nonsolid",
            "vehicle",
            "viewblock",
            "piece"
            });

        // Token: 0x04000056 RID: 86
        private static int Player_Layermask = LayerMask.GetMask(new string[]
        {
            "Default",
            "static_solid",
            "Default_small",
            "piece_nonsolid",
            "terrain",
            "vehicle"
        });
    }
}
