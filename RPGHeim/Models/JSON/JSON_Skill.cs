using System.Collections.Generic;

namespace RPGHeim.Models.JSON
{
    class JSON_Skill
    {
        public string Name;
        public string Identifier;
        public string Description;
        public string Icon;
        public string IncreaseStep;
        public Dictionary<string, string> Translations;
    }
}
