using System.Collections.Generic;
using System.Linq;
using HomeAppliancesStore.Modules.Config;
using HomeAppliancesStore.Shared;

namespace HomeAppliancesStore.Modules.Product
{
    public class ProductService
    {
        private readonly CsvFileRepository<ProductEntity> _productRepository;

        public ProductService()
        {
            _productRepository = new CsvFileRepository<ProductEntity>(
                ConfigConstants.ProductsPath,
                new ProductCsvParser()
            );
        }

        public List<ProductEntity> GetAllProducts()
        {
            return _productRepository.ReadAll();
        }

        public void AddProduct(string name, string category, decimal price, int quantity)
        {
            var products = _productRepository.ReadAll();
            int newId = products.Any() ? products.Max(p => p.Id) + 1 : 1;

            var newProduct = new ProductEntity
            {
                Id = newId,
                Name = name,
                Category = category,
                Price = price,
                Quantity = quantity
            };

            _productRepository.Append(newProduct);
        }

        public void DeleteProduct(int id)
        {
            var products = _productRepository.ReadAll();
            var productToRemove = products.FirstOrDefault(p => p.Id == id);

            if (productToRemove != null)
            {
                products.Remove(productToRemove);
                _productRepository.WriteAll(products, ConfigConstants.ProductsHeader);
            }
        }

        public void EditProduct(int id, string newName, string newCategory, decimal? newPrice, int? newQuantity)
        {
            var products = _productRepository.ReadAll();
            var productToEdit = products.FirstOrDefault(p => p.Id == id);

            if (productToEdit != null)
            {
                if (!string.IsNullOrEmpty(newName)) productToEdit.Name = newName;
                if (!string.IsNullOrEmpty(newCategory)) productToEdit.Category = newCategory;
                if (newPrice.HasValue) productToEdit.Price = newPrice.Value;
                if (newQuantity.HasValue) productToEdit.Quantity = newQuantity.Value;

                _productRepository.WriteAll(products, ConfigConstants.ProductsHeader);
            }
        }
    }
}