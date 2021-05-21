using System.Collections.Generic;

public class SE_CustomModifier : StatusEffect
{
	public float m_blockModifier = 1f;
	public float m_parryModifier = 1f;
	public Dictionary<Skills.SkillType, float> m_modSkills = new Dictionary<Skills.SkillType, float>();

	public override void Setup(Character player)
	{
		foreach (KeyValuePair<Skills.SkillType, float> skillMod in m_modSkills)
		{
			Skills.Skill matchedSkill;
			Player.m_localPlayer.GetSkillFactor(skillMod.Key); // Checking for it first adds it to player
			Player.m_localPlayer.m_skills.m_skillData.TryGetValue(skillMod.Key, out matchedSkill);
			if (matchedSkill != null)
            {
				matchedSkill.m_level = skillMod.Value;
            }
		}
	}
}