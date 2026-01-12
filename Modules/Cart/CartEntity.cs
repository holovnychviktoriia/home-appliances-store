// <copyright file="CartEntity.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

namespace HomeAppliancesStore.Modules.Cart
{
    public class CartEntity
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice => this.Price * this.Quantity;
    }
}
