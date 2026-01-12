// <copyright file="UserEntity.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

namespace HomeAppliancesStore.Modules.User
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public decimal Balance { get; set; }

        public string Role { get; set; } = "Customer";
    }
}
