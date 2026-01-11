using System.IO;

namespace HomeAppliancesStore.Modules.Config
{
    public static class ConfigConstants
    {
        public static readonly string UsersPath = Path.Combine("Database", "users.csv");
        public static readonly string ProductsPath = Path.Combine("Database", "products.csv");

        public const string UsersHeader = "Id,Email,PasswordHash,Balance,Role";
        public const string ProductsHeader = "Id,Name,Category,Price,Quantity";
    }
}