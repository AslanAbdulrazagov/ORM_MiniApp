﻿namespace ORM_MiniApp.Models
{
    public class OrderDetail:BaseEntity
    {
        public int OrderId { get;set; }
        public Order Order { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }
        
    }
}
