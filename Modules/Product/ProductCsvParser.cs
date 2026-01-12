// <copyright file="ProductCsvParser.cs" company="HomeAppliancesStore">
// Copyright (c) HomeAppliancesStore. All rights reserved.
// </copyright>

using HomeAppliancesStore.Shared;

namespace HomeAppliancesStore.Modules.Product
{
    public class ProductCsvParser : ICsvParser<ProductEntity>
    {
        public ProductEntity Parse(string line)
        {
            var parts = line.Split(',');

            return new ProductEntity
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Category = parts[2],
                Price = decimal.Parse(parts[3]),
                Quantity = int.Parse(parts[4]),
            };
        }

        public string ToCsv(ProductEntity entity)
        {
            return $"{entity.Id},{entity.Name},{entity.Category},{entity.Price},{entity.Quantity}";
        }
    }
}
