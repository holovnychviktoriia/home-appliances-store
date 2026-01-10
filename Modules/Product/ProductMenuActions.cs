using System;
using HomeAppliancesStore.Shared;

namespace HomeAppliancesStore.Modules.Product
{
    public class ProductMenuActions
    {
        private readonly ProductService _productService;

        public ProductMenuActions()
        {
            _productService = new ProductService();
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
                    case "1": AddProduct(); break;
                    case "2": EditProduct(); break;
                    case "3": DeleteProduct(); break;
                    case "4": ListProducts(); break;
                    case "0": back = true; break;
                }
            }
        }

        private void ListProducts()
        {
            var list = _productService.GetAllProducts();
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
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Category: ");
            string cat = Console.ReadLine();
            Console.Write("Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price)) return;
            Console.Write("Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int qty)) return;

            _productService.AddProduct(name, cat, price, qty);
            Console.WriteLine("Product added.");
            Console.ReadKey();
        }

        private void EditProduct()
        {
            ListProducts();
            Console.Write("\nEnter ID to edit: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var product = _productService.GetAllProducts().Find(p => p.Id == id);
                if (product == null)
                {
                    Console.WriteLine("Product not found.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"Editing: {product.Name}");
                Console.WriteLine("Enter new values (leave empty to keep current):");
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Category: ");
                string cat = Console.ReadLine();
                Console.Write("Price: ");
                string priceStr = Console.ReadLine();
                decimal? price = string.IsNullOrEmpty(priceStr) ? (decimal?)null : decimal.Parse(priceStr);
                Console.Write("Quantity: ");
                string qtyStr = Console.ReadLine();
                int? qty = string.IsNullOrEmpty(qtyStr) ? (int?)null : int.Parse(qtyStr);

                _productService.EditProduct(id, 
                    string.IsNullOrEmpty(name) ? null : name, 
                    string.IsNullOrEmpty(cat) ? null : cat, 
                    price, qty);
                
                Console.WriteLine("Product edited.");
                Console.ReadKey();
            }
        }

        private void DeleteProduct()
        {
            ListProducts();
            Console.Write("\nEnter ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _productService.DeleteProduct(id);
                Console.WriteLine("Product deleted.");
                Console.ReadKey();
            }
        }
    }
}