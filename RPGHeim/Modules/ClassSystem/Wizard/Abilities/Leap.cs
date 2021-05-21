using HarmonyLib;
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
    public class Launch
    {
        public static void Execute(Player player, ref float altitude)
        {
            float damageModifier = 1;
            var skill = RPGHeim.SkillsManager.GetSkill(SkillsManager.RPGHeimSkill.Wizard).m_skill;
            float skillLevel = RPGHeim.SkillsManager.GetRPGHeimSkillFactor(player, SkillsManager.RPGHeimSkill.Wizard);
            ((ZSyncAnimation)typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(player)).SetTrigger("swing_axe1");
            Vector3 velocity = player.GetVelocity();
            Rigidbody value = Traverse.Create(player).Field("m_body").GetValue<Rigidbody>();
            //Class_Valkyrie.inFlight = true;
            Vector3 zero = Vector3.zero;
            zero.z = value.velocity.z;
            zero.x = value.velocity.x;
            value.velocity = velocity * 2f + new Vector3(0f, 15f, 0f) + zero * 3f;
            Rigidbody rigidbody = value;
            rigidbody.velocity *= 0.8f + skillLevel * 0.005f;
            var GO_CastFX = UnityEngine.Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("sfx_perfectblock"), player.transform.position, Quaternion.identity);
            GO_CastFX = UnityEngine.Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("vfx_perfectblock"), player.transform.position, Quaternion.identity);
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
