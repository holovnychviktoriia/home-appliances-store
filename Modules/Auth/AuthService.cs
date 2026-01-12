// <copyright file="AuthService.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

using System.Linq;
using System.Security.Cryptography;
using System.Text;
using HomeAppliancesStore.Modules.Config;
using HomeAppliancesStore.Modules.User;
using HomeAppliancesStore.Shared;

namespace HomeAppliancesStore.Modules.Auth
{
    public class AuthService
    {
        private readonly CsvFileRepository<UserEntity> userRepository;

        public AuthService()
        {
            this.userRepository = new CsvFileRepository<UserEntity>(
                ConfigConstants.UsersPath,
                new UserCsvParser());
        }

        public UserEntity? Login(string email, string password)
        {
            var users = this.userRepository.ReadAll();
            string passwordHash = this.HashPassword(password);

            return users.FirstOrDefault(u => u.Email == email && u.PasswordHash == passwordHash);
        }

        public bool Register(string email, string password)
        {
            var users = this.userRepository.ReadAll();

            if (users.Any(u => u.Email == email))
            {
                return false;
            }

            int newId = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            string passwordHash = this.HashPassword(password);

            var newUser = new UserEntity
            {
                Id = newId,
                Email = email,
                PasswordHash = passwordHash,
                Balance = 0,
                Role = "Customer",
            };

            this.userRepository.Append(newUser);
            return true;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
