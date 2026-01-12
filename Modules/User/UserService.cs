// <copyright file="UserService.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using HomeAppliancesStore.Modules.Config;
using HomeAppliancesStore.Shared;

namespace HomeAppliancesStore.Modules.User
{
    public class UserService
    {
        private readonly CsvFileRepository<UserEntity> userRepository;

        public UserService()
        {
            this.userRepository = new CsvFileRepository<UserEntity>(
                ConfigConstants.UsersPath,
                new UserCsvParser());
        }

        public List<UserEntity> GetAllUsers()
        {
            return this.userRepository.ReadAll();
        }

        public UserEntity? GetUserById(int id)
        {
            return this.userRepository.ReadAll().FirstOrDefault(u => u.Id == id);
        }

        public void TopUpBalance(int userId, decimal amount)
        {
            var users = this.userRepository.ReadAll();
            var user = users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                user.Balance += amount;
                this.userRepository.WriteAll(users, ConfigConstants.UsersHeader);
            }
        }

        public void UpdateUserBalance(int userId, decimal newBalance)
        {
            var users = this.GetAllUsers();
            var user = users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                user.Balance = newBalance;
                this.userRepository.WriteAll(users, ConfigConstants.UsersHeader);
            }
        }
    }
}
