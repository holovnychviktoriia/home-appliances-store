using System.Collections.Generic;
using System.Linq;

namespace HomeAppliancesStore.Modules.Cart
{
    public class CartService
    {
        private List<CartEntity> _items = new List<CartEntity>();

        public void AddItem(int productId, string productName, decimal price, int quantity)
        {
            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _items.Add(new CartEntity
                {
                    ProductId = productId,
                    ProductName = productName,
                    Price = price,
                    Quantity = quantity
                });
            }
        }

        public void RemoveItem(int productId)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                _items.Remove(item);
            }
        }

        public List<CartEntity> GetItems()
        {
            return _items;
        }

        public decimal GetTotalAmount()
        {
            return _items.Sum(i => i.TotalPrice);
        }

        public void ClearCart()
        {
            _items.Clear();
        }
    }
}