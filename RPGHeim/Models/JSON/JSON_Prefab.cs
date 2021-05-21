using System.Collections.Generic;

namespace RPGHeim.Models.JSON
{
    class JSON_Prefab
    {
        public string Prefab;
        public string Type;
        public RecipeConf RecipeConfig;
        public PieceConf PieceConfig;
        public Dictionary<string, string> Translations;

        public class RecipeConf {
            public string CraftingStation;
            public Dictionary<string, int> Ingredients = new Dictionary<string, int>();
        }

        public class PieceConf {
            public string PieceTable;
            public Dictionary<string, int> Requirements = new Dictionary<string, int>();
        }
    }
}
