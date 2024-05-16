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

        public static IEnumerable<T> DeserializeList<T>(IEnumerable<object> items)
        {
            foreach (var item in items)
            {
                yield return Deserialize<T>(item);
            }
        }
    }
}
