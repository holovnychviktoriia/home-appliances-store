using System.Collections.Generic;
using System.IO;
using System.Linq;
using HomeAppliancesStore.Modules.Order;
using HomeAppliancesStore.Modules.User;

namespace HomeAppliancesStore.Modules.Admin
{
    public class AdminService
    {
        private readonly UserService _userService;
        private readonly string _ordersFile = Path.Combine("Database", "orders.csv");

        public AdminService()
        {
            _userService = new UserService();
        }

        public List<UserEntity> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        public List<OrderEntity> GetAllOrders()
        {
            List<OrderEntity> orders = new List<OrderEntity>();
            if (!File.Exists(_ordersFile)) return orders;

            var lines = File.ReadAllLines(_ordersFile).Skip(1);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(',');
                if (parts.Length < 5) continue;

                try
                {
                    orders.Add(new OrderEntity
                    {
                        Id = int.Parse(parts[0]),
                        UserId = int.Parse(parts[1]),
                        OrderDate = System.DateTime.Parse(parts[2]),
                        TotalAmount = decimal.Parse(parts[3]),
                        OrderDetails = parts[4]
                    });
                }
                catch { continue; }
            }
            return orders;
        }
    }
}