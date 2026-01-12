// <copyright file="AdminMenuActions.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

using System;
using HomeAppliancesStore.Modules.Product;
using HomeAppliancesStore.Modules.User;

namespace HomeAppliancesStore.Modules.Admin
{
    public class AdminMenuActions
    {
        private readonly ProductMenuActions productMenuActions;
        private readonly AdminService adminService;

        public AdminMenuActions()
        {
            this.productMenuActions = new ProductMenuActions();
            this.adminService = new AdminService();
        }

        public void ShowAdminMenu(UserEntity admin)
        {
            bool logout = false;
            while (!logout)
            {
                Console.Clear();
                Console.WriteLine($"--- Admin Panel: {admin.Email} ---");
                Console.WriteLine("1. Manage Products (Add/Edit/Delete)");
                Console.WriteLine("2. View All Users");
                Console.WriteLine("3. View All Orders");
                Console.WriteLine("0. Logout");
                Console.Write("Select option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        this.productMenuActions.ShowMenu();
                        break;
                    case "2":
                        this.ShowAllUsers();
                        break;
                    case "3":
                        this.ShowAllOrders();
                        break;
                    case "0":
                        logout = true;
                        break;
                }
            }
        }

        private void ShowAllUsers()
        {
            Console.WriteLine("\n--- All Registered Users ---");
            var users = this.adminService.GetAllUsers();

            Console.WriteLine("{0,-5} {1,-25} {2,-10} {3,-10}", "ID", "Email", "Role", "Balance");
            foreach (var u in users)
            {
                Console.WriteLine("{0,-5} {1,-25} {2,-10} {3,-10}", u.Id, u.Email, u.Role, u.Balance);
            }

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        private void ShowAllOrders()
        {
            Console.WriteLine("\n--- Order History ---");
            var orders = this.adminService.GetAllOrders();

            if (orders.Count == 0)
            {
                Console.WriteLine("No orders found.");
                Console.ReadKey();
                return;
            }

            foreach (var o in orders)
            {
                Console.WriteLine($"ID: {o.Id} | Date: {o.OrderDate} | UserID: {o.UserId} | Total: {o.TotalAmount}");
                Console.WriteLine($"Details: {o.OrderDetails}");
                Console.WriteLine("------------------------------------------------");
            }

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
    }
}
