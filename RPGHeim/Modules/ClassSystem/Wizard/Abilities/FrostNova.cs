using RPGHeim.Modules.StatusEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPGHeim.Modules.ClassSystem.Wizard.Abilities
{
    public class FrostNova
    {

        public static void Execute(Player player, ref float altitude)
        {
            float damageModifier = 1;
            var skill = RPGHeim.SkillsManager.GetSkill(SkillsManager.RPGHeimSkill.Wizard).m_skill;
            float skillLevel = RPGHeim.SkillsManager.GetRPGHeimSkillFactor(player, SkillsManager.RPGHeimSkill.Wizard);
            ((ZSyncAnimation)typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(player)).SetTrigger("swing_axe1");
            UnityEngine.Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("fx_guardstone_activate"), player.transform.position, Quaternion.identity);
            bool flag9 = player.GetSEMan().HaveStatusEffect("Burning");
            if (flag9)
            {
                player.GetSEMan().RemoveStatusEffect("Burning", false);
            }
            List<Character> allCharacters = Character.GetAllCharacters();
            foreach (Character character in allCharacters)
            {
                bool flag10 = BaseAI.IsEnemy(player, character) && (character.transform.position - player.transform.position).magnitude <= 10f + 0.1f * skillLevel;
                if (flag10)
                {
                    Vector3 vector2 = character.transform.position - player.transform.position;
                    HitData hitData2 = new HitData();
                    hitData2.m_damage.m_frost = UnityEngine.Random.Range(10f + 0.5f * skillLevel, 20f + skillLevel) * damageModifier;
                    hitData2.m_pushForce = 50f;
                    hitData2.m_point = character.GetEyePoint();
                    hitData2.m_dir = player.transform.position - character.transform.position;
                    hitData2.m_skill = skill;
                    character.ApplyDamage(hitData2, true, true, HitData.DamageModifier.Normal);
                    SE_Slow se_Slow = (SE_Slow)ScriptableObject.CreateInstance(typeof(SE_Slow));
                    character.GetSEMan().AddStatusEffect(se_Slow.name, true);
                }
            }
            player.RaiseSkill(skill, 1);
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
