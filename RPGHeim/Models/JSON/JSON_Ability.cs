using System.Collections.Generic;

namespace RPGHeim.Models.JSON
{
    class JSON_Ability
    {
        public string Name;
        public string Tooltip;
        public string Type;
        public string Skill;
        public string CooldownMax;
        public string StaminaCost;
        public string Icon;
        public Effect[] Effects;
        public Dictionary<string, string> Translations;

        public class Effect
        {
            public string Target;
            public string DamageValue;
            public string DamageType;
            public string HealValue;
            public Ability.Effect.StatusEffect[] StatusEffects;
        }
    }
}
