// <copyright file="ProductMenuActions.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

using System;
using HomeAppliancesStore.Shared;

namespace HomeAppliancesStore.Modules.Product
{
    public class ProductMenuActions
    {
        private readonly ProductService productService;

        public ProductMenuActions()
        {
            this.productService = new ProductService();
        }

        public void ShowMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("--- Product Management ---");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Edit Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. List Products");
                Console.WriteLine("0. Back");
                Console.Write("Select: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        this.AddProduct();
                        break;
                    case "2":
                        this.EditProduct();
                        break;
                    case "3":
                        this.DeleteProduct();
                        break;
                    case "4":
                        this.ListProducts();
                        break;
                    case "0":
                        back = true;
                        break;
                }
            }
        }

        private void ListProducts()
        {
            var list = this.productService.GetAllProducts();
            Console.WriteLine("\n--- Product List ---");
            Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-10} {4,-10}", "ID", "Name", "Category", "Price", "Qty");
            Console.WriteLine(new string('-', 70));
            foreach (var p in list)
            {
                Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-10} {4,-10}", p.Id, p.Name, p.Category, p.Price, p.Quantity);
            }

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        private void AddProduct()
        {
            Console.WriteLine("\n--- Add Product ---");

            string name = this.GetValidString("Name");
            string category = this.GetValidString("Category");
            decimal price = this.GetValidDecimal("Price");
            int quantity = this.GetValidInt("Quantity");

            this.productService.AddProduct(name, category, price, quantity);
            Console.WriteLine("Product added successfully.");
            Console.ReadKey();
        }

        private void EditProduct()
        {
            this.ListProducts();
            Console.Write("\nEnter ID to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("Invalid ID.");
                Console.ReadKey();
                return;
            }

            var product = this.productService.GetAllProducts().Find(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Editing: {product.Name}");
            Console.WriteLine("Enter new values (leave empty to keep current):");

            Console.Write("Name: ");
            string? name = Console.ReadLine();

            Console.Write("Category: ");
            string? category = Console.ReadLine();

            Console.Write("Price (current: " + product.Price + "): ");
            string? priceStr = Console.ReadLine();
            decimal? price = null;
            if (!string.IsNullOrWhiteSpace(priceStr) && decimal.TryParse(priceStr, out decimal pVal) && pVal > 0)
            {
                price = pVal;
            }

            Console.Write("Quantity (current: " + product.Quantity + "): ");
            string? qtyStr = Console.ReadLine();
            int? qty = null;
            if (!string.IsNullOrWhiteSpace(qtyStr) && int.TryParse(qtyStr, out int qVal) && qVal >= 0)
            {
                qty = qVal;
            }

            this.productService.EditProduct(
                id,
                string.IsNullOrEmpty(name) ? null : name,
                string.IsNullOrEmpty(category) ? null : category,
                price,
                qty);

            Console.WriteLine("Product edited.");
            Console.ReadKey();
        }

        private void DeleteProduct()
        {
            this.ListProducts();
            Console.Write("\nEnter ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id) && id > 0)
            {
                this.productService.DeleteProduct(id);
                Console.WriteLine("Product deleted.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }

            Console.ReadKey();
        }

        private string GetValidString(string fieldName)
        {
            while (true)
            {
                Console.Write($"{fieldName}: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                Console.WriteLine($"{fieldName} cannot be empty.");
            }
        }

        private decimal GetValidDecimal(string fieldName)
        {
            while (true)
            {
                Console.Write($"{fieldName}: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal value) && value > 0)
                {
                    return value;
                }

                Console.WriteLine($"{fieldName} must be a positive number.");
            }
        }

        private int GetValidInt(string fieldName)
        {
            while (true)
            {
                Console.Write($"{fieldName}: ");
                if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
                {
                    return value;
                }

                Console.WriteLine($"{fieldName} must be a non-negative integer.");
            }
        }
    }
}
