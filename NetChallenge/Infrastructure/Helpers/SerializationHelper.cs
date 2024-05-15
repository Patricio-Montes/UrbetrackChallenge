using Newtonsoft.Json;
using System.Collections.Generic;

namespace NetChallenge.Infrastructure.Helpers
{
    public static class SerializationHelper
    {
        public static T Deserialize<T>(object item)
        {
            var json = JsonConvert.SerializeObject(item);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static List<T> DeserializeList<T>(IEnumerable<object> items)
        {
            var resultList = new List<T>();

            foreach (var item in items)
            {
                var deserializedItem = Deserialize<T>(item);
                resultList.Add(deserializedItem);
            }

            return resultList;
        }
    }
}
