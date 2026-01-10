namespace HomeAppliancesStore.Modules.Config
{
    public static class ConfigConstants
    {
        public const string UsersPath = "users.csv";
        public const string ProductsPath = "products.csv";

        public const string UsersHeader = "Id,Email,PasswordHash,Balance,Role";
        public const string ProductsHeader = "Id,Name,Category,Price,Quantity";
    }
}