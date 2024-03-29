﻿
namespace VegeShama.Common.DatabaseModels.RavenDB
{
    public class Order
    {
        public string Id { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Payment Payment { get; set; }
        public List<Product> Products { get; set; }
        public Address Address { get; set; }
    }
}
