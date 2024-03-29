﻿using EFCoreDal = VegeShama.Common.DatabaseModels.EFCore;
using RavenDBDal = VegeShama.Common.DatabaseModels.RavenDB;
using RelationalDal = VegeShama.Common.DatabaseModels.Relational;

namespace VegeShama.Common.DomainModels
{
    public class Order
    {
        public Guid Id { get; }
        public DateOnly DueDate { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public Address Address { get; set; }
        public Payment Payment { get; set; }
        public List<Product> Products { get; set; }

        public Order(RavenDBDal.Order order)
        {
            Id = Guid.Parse(order.Id);
            DueDate = DateOnly.FromDateTime(order.DueDate);
            DeliveryDate = DateOnly.FromDateTime(order.DeliveryDate.Date);
            Address = new Address(order.Address);
            Payment = new Payment(order.Payment);
            Products = order.Products.Select(x => new Product(x)).ToList();
        }

        public Order(EFCoreDal.Order order)
        {
            Id = order.Id;
            DueDate = DateOnly.FromDateTime(order.DueDate);
            DeliveryDate = DateOnly.FromDateTime(order.DeliveryDate.Date);
            Address = new Address(order.Address);
            Payment = new Payment(order.Payment);
            Products = order.OrderProducts.Select(x => new Product(x.Product)).ToList();
        }

        public Order(RelationalDal.Order order)
        {
            Id = order.Id;
            DueDate = DateOnly.FromDateTime(order.DueDate);
            DeliveryDate = DateOnly.FromDateTime(order.DeliveryDate.Date);
            Address = new Address(order.Address);
            Payment = new Payment(order.Payment);
            Products = order.Products.Select(x => new Product(x)).ToList();
        }
    }
}
