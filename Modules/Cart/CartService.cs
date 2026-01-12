// <copyright file="CartService.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;

namespace HomeAppliancesStore.Modules.Cart
{
    public class CartService
    {
        private readonly List<CartEntity> items = new List<CartEntity>();

        public void AddItem(int productId, string productName, decimal price, int quantity)
        {
            var existingItem = this.items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                this.items.Add(new CartEntity
                {
                    ProductId = productId,
                    ProductName = productName,
                    Price = price,
                    Quantity = quantity,
                });
            }
        }

        public void RemoveItem(int productId)
        {
            var item = this.items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                this.items.Remove(item);
            }
        }

        public List<CartEntity> GetItems()
        {
            return this.items;
        }

        public decimal GetTotalAmount()
        {
            return this.items.Sum(i => i.TotalPrice);
        }

        public void ClearCart()
        {
            this.items.Clear();
        }
    }
}
