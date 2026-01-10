using System.IO;
using HomeAppliancesStore.Modules.Config;

namespace HomeAppliancesStore.Shared
{
    public class DatabaseConfig
    {
        public static void Initialize()
        {
            EnsureFileExists(ConfigConstants.UsersPath, ConfigConstants.UsersHeader);
            EnsureFileExists(ConfigConstants.ProductsPath, ConfigConstants.ProductsHeader);
            
            SeedDefaultData();
        }

        private static void EnsureFileExists(string path, string header)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, header + "\n");
            }
        }

        private static void SeedDefaultData()
        {
            var lines = File.ReadAllLines(ConfigConstants.UsersPath);

            if (lines.Length <= 1)
            {
                string passwordHash = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918";
                string defaultAdmin = $"1,admin@store.com,{passwordHash},0,Admin";

                File.AppendAllText(ConfigConstants.UsersPath, defaultAdmin + "\n");
            }
        }
    }
}