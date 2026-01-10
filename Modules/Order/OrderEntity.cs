using System;

namespace HomeAppliancesStore.Modules.Order
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderDetails { get; set; }
    }
}