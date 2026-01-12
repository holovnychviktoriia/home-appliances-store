// <copyright file="OrderService.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HomeAppliancesStore.Modules.Cart;
using HomeAppliancesStore.Modules.Product;
using HomeAppliancesStore.Modules.User;

namespace HomeAppliancesStore.Modules.Order
{
    public class OrderService
    {
        private readonly string ordersFile = Path.Combine("Database", "orders.csv");
        private readonly ProductService productService;
        private readonly UserService userService;

        public OrderService()
        {
            this.productService = new ProductService();
            this.userService = new UserService();
            this.EnsureFileExists();
        }

        public string ProcessOrder(UserEntity user, List<CartEntity> cartItems)
        {
            if (cartItems.Count == 0)
            {
                return "Cart is empty.";
            }

            decimal totalAmount = cartItems.Sum(i => i.TotalPrice);

            if (user.Balance < totalAmount)
            {
                return "Insufficient funds on balance!";
            }

            var allProducts = this.productService.GetAllProducts();
            foreach (var item in cartItems)
            {
                var productInStock = allProducts.FirstOrDefault(p => p.Id == item.ProductId);
                if (productInStock == null || productInStock.Quantity < item.Quantity)
                {
                    return $"Not enough stock for product '{item.ProductName}'.";
                }
            }

            foreach (var item in cartItems)
            {
                var product = allProducts.First(p => p.Id == item.ProductId);
                int newQuantity = product.Quantity - item.Quantity;
                this.productService.EditProduct(product.Id, null, null, null, newQuantity);
            }

            decimal newBalance = user.Balance - totalAmount;
            this.userService.UpdateUserBalance(user.Id, newBalance);
            user.Balance = newBalance;

            this.SaveOrderToFile(user.Id, totalAmount, cartItems);

            return "Success";
        }

        private void EnsureFileExists()
        {
            string? directory = Path.GetDirectoryName(this.ordersFile);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(this.ordersFile))
            {
                File.WriteAllText(this.ordersFile, "Id,UserId,OrderDate,TotalAmount,Details\n");
            }
        }

        private void SaveOrderToFile(int userId, decimal totalAmount, List<CartEntity> cartItems)
        {
            int newId = this.GetNextId(this.ordersFile);

            StringBuilder detailsBuilder = new StringBuilder();
            foreach (var item in cartItems)
            {
                detailsBuilder.Append($"{item.ProductName} x{item.Quantity}; ");
            }

            string details = detailsBuilder.ToString().TrimEnd(' ', ';');

            string line = $"{newId},{userId},{DateTime.Now},{totalAmount},{details}";
            File.AppendAllText(this.ordersFile, line + Environment.NewLine);
        }

        private int GetNextId(string path)
        {
            if (!File.Exists(path))
            {
                return 1;
            }

            var lines = File.ReadAllLines(path);
            if (lines.Length <= 1)
            {
                return 1;
            }

            int max = 0;
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (int.TryParse(parts[0], out int id))
                {
                    if (id > max)
                    {
                        max = id;
                    }
                }
            }

            return max + 1;
        }
    }
}
