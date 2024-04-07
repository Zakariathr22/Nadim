using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.Services
{
    // This is a static class named ConfigurationService. Static classes cannot be instantiated.
    public static class ConfigurationService
    {
        // Declare a private static field of type ConfigurationBuilder.
        private static ConfigurationBuilder builder;
        // Declare a private static field of type IConfigurationRoot.
        private static IConfigurationRoot configuration;

        // This is a public static method named Configure. It takes a string parameter named setting.
        public static void Configure(string setting)
        {
            // Instantiate a new ConfigurationBuilder and assign it to the builder field.
            builder = new ConfigurationBuilder();
            // Combine the base directory of the application domain with the relative path to get the absolute path of the app settings.
            string appSettingsPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../../../");
            // Set the base path of the builder to the app settings path.
            FileConfigurationExtensions.SetBasePath(builder, appSettingsPath);
            // Add a JSON configuration source to the builder.
            JsonConfigurationExtensions.AddJsonFile(builder, $"{setting}.json", false, true);
            // Build the configuration and assign it to the configuration field.
            configuration = builder.Build();
        }

        // This is a public static method named GetCoGetConnectionString. It takes a string parameter named name.
        public static string GetCoGetConnectionString(string name)
        {
            // Call the Configure method with "appsettings" as the argument.
            Configure("appsettings");
            // Return the connection string from the configuration with the specified name.
            return configuration.GetConnectionString(name);
        }

        // This is a public static method named GetAppSetting. It takes a string parameter named name.
        public static string GetAppSetting(string name)
        {
            // Call the Configure method with "appsettings" as the argument.
            Configure("appsettings");
            // Return the app setting from the configuration with the specified name.
            return configuration[$"AppSettings:{name}"];
        }
    }
}
