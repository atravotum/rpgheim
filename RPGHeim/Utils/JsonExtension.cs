using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeim
{
    static class JsonUtil<T>
    {
        public static List<T> ListFromJson(string json)
        {
            return SimpleJson.SimpleJson.DeserializeObject<List<T>>(json);
        }

        public static string JsonifyModels(List<T> models)
        {
            return SimpleJson.SimpleJson.SerializeObject(models);
        }
    }
}
