using Newtonsoft.Json;

namespace CH.DVDCentral.UI.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession sesssion, string key, object value)
        {
            sesssion.SetString(key, JsonConvert.SerializeObject(value));
        }

        //We don't know what T is 
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
