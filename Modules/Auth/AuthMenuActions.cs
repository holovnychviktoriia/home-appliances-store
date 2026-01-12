// <copyright file="AuthMenuActions.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

using System;
using HomeAppliancesStore.Modules.User;
using HomeAppliancesStore.Shared;

namespace HomeAppliancesStore.Modules.Auth
{
    public class AuthMenuActions
    {
        private readonly AuthService authService;

        public AuthMenuActions()
        {
            this.authService = new AuthService();
        }

        public UserEntity? Authenticate()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Welcome to Home Appliances Store ---");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("0. Exit");
                Console.Write("Select option: ");

                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        var user = this.Login();
                        if (user != null)
                        {
                            return user;
                        }

                        break;
                    case "2":
                        this.Register();
                        break;
                    case "0":
                        return null;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private UserEntity? Login()
        {
            Console.WriteLine("\n--- Login ---");
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? string.Empty;
            Console.Write("Password: ");
            string password = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Email and password cannot be empty.");
                Console.ReadKey();
                return null;
            }

            var user = this.authService.Login(email, password);
            if (user != null)
            {
                Console.WriteLine($"Welcome back, {user.Email}!");
                System.Threading.Thread.Sleep(1000);
                return user;
            }

            Console.WriteLine("Invalid email or password.");
            Console.ReadKey();
            return null;
        }

        private void Register()
        {
            Console.WriteLine("\n--- Register ---");
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? string.Empty;

            if (!ValidationUtils.IsValidEmail(email))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid email format! (Example: user@mail.com)");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.Write("Password: ");
            string password = Console.ReadLine() ?? string.Empty;

            if (!ValidationUtils.IsValidPassword(password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Password is too short (min 4 chars).");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            bool success = this.authService.Register(email, password);
            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Registration successful! You can now login.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User with this email already exists.");
                Console.ResetColor();
            }

            Console.ReadKey();
        }
    }
}
