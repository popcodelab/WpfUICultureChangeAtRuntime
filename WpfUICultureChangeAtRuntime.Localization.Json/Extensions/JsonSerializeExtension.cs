using Newtonsoft.Json;

namespace WpfUICultureChangeAtRuntime.Localization.Json.Extensions
{
    public static class JsonSerializeExtension
    {
        /// <summary>
        /// Default serializer settings
        /// </summary>
        private static readonly JsonSerializerSettings _defaultSerializerSettings = GetDefaultSerializerSettings();

        private static JsonSerializerSettings GetDefaultSerializerSettings() => new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
        };

        /// <summary>
        /// Transforms the Json into a T object
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="jsonString">Json to be transformed into an Object</param>
        /// <returns>T object</returns>
        public static T JsonToObject<T>(this string jsonString) => jsonString.JsonToObject<T>(null);

        /// <summary>
        /// Deserializes the Json string
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="jsonString">Json to be deserialized</param>
        /// <param name="settings">JsonSerializerSettings</param>
        /// <returns>T object from deserialization</returns>
        public static T JsonToObject<T>(this string jsonString, JsonSerializerSettings? settings) => JsonConvert.DeserializeObject<T>(jsonString, settings ?? _defaultSerializerSettings);


    }
}
