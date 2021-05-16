using Jotunn.Configs;
using Jotunn.Managers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGHeim
{
    public static class SkillsManager
    {
        public class SkillConfigExt : SkillConfig
        {
            public RPGHeimSkill RPGHeimSkill { get; set; }
        }

        public enum RPGHeimSkill
        {
            // Classes
            Fighter,
            Wizard,
            Healer,
            Rogue,

            // Class Specific Skills?
        }

        /// <summary>
        /// Extension for Player to get custom skills by enumeration.
        /// Usage: player.GetRPGHeimSkillFactor(RPGHeimSkill.Fighter);
        /// </summary>
        /// <param name="player">Instance of the player(not needed to be passed)</param>
        /// <param name="skill">RPGHeimSkill enumeration</param>
        /// <returns></returns>
        public static float GetRPGHeimSkillFactor(this Player player, RPGHeimSkill skill)
        {
            var skillDef = GetSkill(skill);
            return player.GetSkillFactor(skillDef.m_skill);
        }

        public static Skills.SkillDef GetSkill(RPGHeimSkill skill)
        {
            return SkillManager.Instance.GetSkill(SkillDefsByEnum[skill].Identifier);
        }

        public static void RegisterSkills()
        {
            var allCustomSkills = Enum.GetValues(typeof(RPGHeimSkill)).Cast<RPGHeimSkill>();
            foreach (var customSkillEnum in allCustomSkills)
            {
                SkillConfigExt skillConfig = null;
                switch (customSkillEnum)
                {
                    case RPGHeimSkill.Fighter:
                        skillConfig = new SkillConfigExt
                        {
                            Identifier = "github.atravotum.rpgheim.skills.fighter",
                            Name = "Fighter",
                            Description = "Your current skill as a master of war.",
                            Icon = AssetManager.GetResourceSprite(AssetManager.SpriteAssets.FighterIcon),
                            IncreaseStep = 1f,
                        };
                        break;
                    case RPGHeimSkill.Wizard:
                        skillConfig = new SkillConfigExt
                        {
                            Identifier = "github.atravotum.rpgheim.skills.wizard",
                            Name = "Wizard",
                            Description = "Your current skill as a master of elements.",
                            Icon = AssetManager.GetResourceSprite(AssetManager.SpriteAssets.FighterIcon),
                            IncreaseStep = 1f,
                        };
                        break;
                    case RPGHeimSkill.Healer:
                        skillConfig = new SkillConfigExt
                        {
                            Identifier = "github.atravotum.rpgheim.skills.healer",
                            Name = "Healer",
                            Description = "Your current skill as a master of healing.",
                            Icon = AssetManager.GetResourceSprite(AssetManager.SpriteAssets.FighterIcon),
                            IncreaseStep = 1f,
                        };
                        break;
                    case RPGHeimSkill.Rogue:
                        skillConfig = new SkillConfigExt
                        {
                            Identifier = "github.atravotum.rpgheim.skills.rogue",
                            Name = "Rogue",
                            Description = "Your current skill as a master of sealth.",
                            Icon = AssetManager.GetResourceSprite(AssetManager.SpriteAssets.FighterIcon),
                            IncreaseStep = 1f,
                        };
                        break;
                    default:
                        break;
                }
                if (skillConfig == null) continue;
                Jotunn.Logger.LogInfo($"Registering: {customSkillEnum}.");
                SkillManager.Instance.AddSkill(skillConfig);
                SkillDefsByEnum.Add(customSkillEnum, skillConfig);
            }
        }

        public static Dictionary<RPGHeimSkill, SkillConfigExt> SkillDefsByEnum = new Dictionary<RPGHeimSkill, SkillConfigExt>();
    }
}
