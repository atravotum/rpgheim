// SE_Stats
using System.Collections.Generic;
using UnityEngine;

public class SE_CustomModifier : StatusEffect
{
	public float m_blockModifier = 1f;
	public float m_parryModifier = 1f;
	public Dictionary<Skills.SkillType, float> m_modSkills = new Dictionary<Skills.SkillType, float>();

	public override void Setup(Character player)
	{
		foreach (KeyValuePair<Skills.SkillType, float> skillMod in m_modSkills)
		{
			player.RaiseSkill(skillMod.Key, skillMod.Value);
		}
	}
}