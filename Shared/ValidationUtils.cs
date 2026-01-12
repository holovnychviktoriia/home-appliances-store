// <copyright file="ValidationUtils.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>
using System.Text.RegularExpressions;

namespace HomeAppliancesStore.Shared
{
    public static class ValidationUtils
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 4;
        }

        public static bool IsPositiveNumber(int number)
        {
            return number > 0;
        }

        public static bool IsPositiveNumber(decimal number)
        {
            return number > 0;
        }

        public static bool IsValidString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}
