// <copyright file="ConfigConstants.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

using System.IO;

namespace HomeAppliancesStore.Modules.Config
{
    public static class ConfigConstants
    {
        public const string UsersHeader = "Id,Email,PasswordHash,Balance,Role";
        public const string ProductsHeader = "Id,Name,Category,Price,Quantity";

        public static readonly string UsersPath = Path.Combine("Database", "users.csv");
        public static readonly string ProductsPath = Path.Combine("Database", "products.csv");
    }
}
