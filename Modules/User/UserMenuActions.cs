using System;
using HomeAppliancesStore.Modules.Cart;
using HomeAppliancesStore.Modules.Order;
using HomeAppliancesStore.Modules.Product;
using HomeAppliancesStore.Shared;

namespace HomeAppliancesStore.Modules.User
{
    public class UserMenuActions
    {
        private readonly ProductService _productService;
        private readonly UserService _userService;
        private readonly CartService _cartService;
        private readonly OrderService _orderService;

        public UserMenuActions()
        {
            _productService = new ProductService();
            _userService = new UserService();
            _cartService = new CartService();
            _orderService = new OrderService();
        }

        public void ShowUserMenu(UserEntity user)
        {
            bool logout = false;
            while (!logout)
            {
                Console.Clear();
                Console.WriteLine($"--- Customer Menu: {user.Email} ---");
                Console.WriteLine($"Balance: {user.Balance} UAH");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Add to Cart");
                Console.WriteLine("3. View Cart & Checkout");
                Console.WriteLine("4. Top Up Balance");
                Console.WriteLine("0. Logout");
                Console.Write("Select option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ShowProducts(); break;
                    case "2": AddToCart(); break;
                    case "3": ViewCartAndCheckout(user); break;
                    case "4": TopUpBalance(user); break;
                    case "0": logout = true; break;
                }
            }
        }

        private void ShowProducts()
        {
            var products = _productService.GetAllProducts();
            Console.WriteLine("\n--- Products ---");
            Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-10}", "ID", "Name", "Price", "Stock");
            foreach (var p in products)
            {
                Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-10}", p.Id, p.Name, p.Price, p.Quantity);
            }
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        private void AddToCart()
        {
            Console.WriteLine("\n--- Add to Cart ---");
            ShowProducts();
            Console.Write("\nEnter Product ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            var product = _productService.GetAllProducts().Find(p => p.Id == id);
            if (product == null) { Console.WriteLine("Product not found."); Console.ReadKey(); return; }

            Console.Write($"Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0) return;

            if (qty > product.Quantity) { Console.WriteLine("Not enough stock!"); Console.ReadKey(); return; }

            _cartService.AddItem(product.Id, product.Name, product.Price, qty);
            Console.WriteLine("Added to cart.");
            Console.ReadKey();
        }

        private void ViewCartAndCheckout(UserEntity user)
        {
            Console.Clear();
            Console.WriteLine("--- Your Cart ---");
            var items = _cartService.GetItems();
            if (items.Count == 0) { Console.WriteLine("Cart is empty."); Console.ReadKey(); return; }

            Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-10} {4,-10}", "ID", "Name", "Price", "Qty", "Total");
            foreach (var item in items)
            {
                Console.WriteLine("{0,-5} {1,-20} {2,-10} {3,-10} {4,-10}", item.ProductId, item.ProductName, item.Price, item.Quantity, item.TotalPrice);
            }
            Console.WriteLine($"\nTotal Amount: {_cartService.GetTotalAmount()} UAH");
            Console.WriteLine("\n1. Checkout (Buy)\n2. Clear Cart\n0. Back");
            
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                string result = _orderService.ProcessOrder(user, items);
                if (result == "Success") { _cartService.ClearCart(); Console.WriteLine("Order processed successfully!"); }
                else { Console.WriteLine($"Order failed: {result}"); }
                Console.ReadKey();
            }
            else if (choice == "2") { _cartService.ClearCart(); Console.WriteLine("Cart cleared."); Console.ReadKey(); }
        }

        private void TopUpBalance(UserEntity user)
        {
            Console.WriteLine("\n--- Top Up Balance ---");
            Console.Write("Enter amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                decimal newBalance = user.Balance + amount;
                _userService.UpdateUserBalance(user.Id, newBalance);
                user.Balance = newBalance;
                Console.WriteLine($"Balance updated. Current: {user.Balance} UAH");
            }
            else { Console.WriteLine("Invalid amount."); }
            Console.ReadKey();
        }
    }
}