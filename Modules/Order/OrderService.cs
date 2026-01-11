using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HomeAppliancesStore.Modules.Cart;
using HomeAppliancesStore.Modules.Product;
using HomeAppliancesStore.Shared;
using HomeAppliancesStore.Modules.User;

namespace HomeAppliancesStore.Modules.Order
{
    public class OrderService
    {
        private readonly string _ordersFile = Path.Combine("Database", "orders.csv");
        private readonly ProductService _productService;
        private readonly UserService _userService;

        public OrderService()
        {
            _productService = new ProductService();
            _userService = new UserService();
            EnsureFileExists();
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(_ordersFile))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_ordersFile));
                File.WriteAllText(_ordersFile, "Id,UserId,OrderDate,TotalAmount,Details\n");
            }
        }

        public string ProcessOrder(UserEntity user, List<CartEntity> CartEntity)
        {
            if (CartEntity.Count == 0) return "Cart is empty.";

            decimal totalAmount = CartEntity.Sum(i => i.TotalPrice);

            if (user.Balance < totalAmount)
            {
                return "Insufficient funds on balance!";
            }

            var allProducts = _productService.GetAllProducts();
            foreach (var item in CartEntity)
            {
                var productInStock = allProducts.FirstOrDefault(p => p.Id == item.ProductId);
                if (productInStock == null || productInStock.Quantity < item.Quantity)
                {
                    return $"Not enough stock for product '{item.ProductName}'.";
                }
            }

            foreach (var item in CartEntity)
            {
                var product = allProducts.First(p => p.Id == item.ProductId);
                int newQuantity = product.Quantity - item.Quantity;
                _productService.EditProduct(product.Id, null, null, null, newQuantity);
            }

            decimal newBalance = user.Balance - totalAmount;
            _userService.UpdateUserBalance(user.Id, newBalance);
            user.Balance = newBalance; 

            SaveOrderToFile(user.Id, totalAmount, CartEntity);

            return "Success";
        }

        private void SaveOrderToFile(int userId, decimal totalAmount, List<CartEntity> CartEntity)
        {
            int newId = GetNextId(_ordersFile);
            
            StringBuilder detailsBuilder = new StringBuilder();
            foreach(var item in CartEntity)
            {
                detailsBuilder.Append($"{item.ProductName} x{item.Quantity}; ");
            }
            string details = detailsBuilder.ToString().TrimEnd(' ', ';');

            string line = $"{newId},{userId},{DateTime.Now},{totalAmount},{details}";
            File.AppendAllText(_ordersFile, line + Environment.NewLine);
        }

        private int GetNextId(string path)
        {
            if (!File.Exists(path)) return 1;
            var lines = File.ReadAllLines(path);
            if (lines.Length <= 1) return 1;
            int max = 0;
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (int.TryParse(parts[0], out int id))
                {
                    if (id > max) max = id;
                }
            }
            return max + 1;
        }
    }
}