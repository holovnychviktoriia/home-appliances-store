// <copyright file="ProductService.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using HomeAppliancesStore.Modules.Config;
using HomeAppliancesStore.Shared;

namespace HomeAppliancesStore.Modules.Product
{
    public class ProductService
    {
        private readonly CsvFileRepository<ProductEntity> productRepository;

        public ProductService()
        {
            this.productRepository = new CsvFileRepository<ProductEntity>(
                ConfigConstants.ProductsPath,
                new ProductCsvParser());
        }

        public List<ProductEntity> GetAllProducts()
        {
            return this.productRepository.ReadAll();
        }

        public void AddProduct(string name, string category, decimal price, int quantity)
        {
            // Validation Logic
            if (string.IsNullOrWhiteSpace(name) || price <= 0 || quantity < 0)
            {
                return;
            }

            var products = this.productRepository.ReadAll();
            int newId = products.Any() ? products.Max(p => p.Id) + 1 : 1;

            var newProduct = new ProductEntity
            {
                Id = newId,
                Name = name,
                Category = category,
                Price = price,
                Quantity = quantity,
            };

            this.productRepository.Append(newProduct);
        }

        public void DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return;
            }

            var products = this.productRepository.ReadAll();
            var productToRemove = products.FirstOrDefault(p => p.Id == id);

            if (productToRemove != null)
            {
                products.Remove(productToRemove);
                this.productRepository.WriteAll(products, ConfigConstants.ProductsHeader);
            }
        }

        public void EditProduct(int id, string? newName, string? newCategory, decimal? newPrice, int? newQuantity)
        {
            if (id <= 0)
            {
                return;
            }

            var products = this.productRepository.ReadAll();
            var productToEdit = products.FirstOrDefault(p => p.Id == id);

            if (productToEdit != null)
            {
                if (!string.IsNullOrEmpty(newName))
                {
                    productToEdit.Name = newName;
                }

                if (!string.IsNullOrEmpty(newCategory))
                {
                    productToEdit.Category = newCategory;
                }

                if (newPrice.HasValue && newPrice.Value > 0)
                {
                    productToEdit.Price = newPrice.Value;
                }

                if (newQuantity.HasValue && newQuantity.Value >= 0)
                {
                    productToEdit.Quantity = newQuantity.Value;
                }

                this.productRepository.WriteAll(products, ConfigConstants.ProductsHeader);
            }
        }
    }
}
