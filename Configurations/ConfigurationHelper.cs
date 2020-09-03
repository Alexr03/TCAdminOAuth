using System;
using Newtonsoft.Json;

namespace TCAdminOAuth.Configurations
{
    public static class ConfigurationHelper
    {
        public static T GetConfiguration<T>(string configName)
        {
            configName = $"{configName.Replace(" ", string.Empty)}";
            var config = TCAdmin.SDK.Utility.GetDatabaseValue(configName);
            if (string.IsNullOrEmpty(config))
            {
                config = JsonConvert.SerializeObject((T) Activator.CreateInstance(typeof(T)), Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Populate,
                        NullValueHandling = NullValueHandling.Include,
                    });
                TCAdmin.SDK.Utility.SetDatabaseValue(configName, config);
            }

            var configText = config;
            return JsonConvert.DeserializeObject<T>(configText);
        }
        
        public static void SetConfiguration(string configName, object value)
        {
            configName = $"{configName.Replace(" ", string.Empty)}";
            if (value != null)
            {
                TCAdmin.SDK.Utility.SetDatabaseValue(configName, JsonConvert.SerializeObject(value));
            }
        }
    }
}