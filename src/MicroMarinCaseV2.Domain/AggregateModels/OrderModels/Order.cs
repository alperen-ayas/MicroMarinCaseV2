using MicroMarinCaseV2.Domain.AggregateModels.CustomerModels;
using MicroMarinCaseV2.Domain.SeedWorks;
using MicroMarinCaseV2.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MicroMarinCaseV2.Domain.AggregateModels.OrderModels
{
    public class Order : AggregateRoot
    {
        public override Guid Id { get ; set ; }
        public double TotalPrice { get; set; }
        public Address Address { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        private Order(Guid id, Address address, Guid customerId, List<OrderItem> orderItems)
        {
            Id = id;
            Address = address;
            CustomerId = customerId;
            OrderItems = orderItems;
        }

        protected Order()
        {
            
        }
        // For Unit Tests
        public Order(Guid id, double totalPrice)
        {
            Id = id;
            TotalPrice = totalPrice;
        }

        public static Order Create(Guid id, Address address, Guid customerId, List<OrderItem> orderItems)
        {
            var order = new Order(id,  address, customerId, orderItems);
            order.CalculateTotalPrice();
            return order;
        }

        public void UpdateAddress(Address address)
        {
            Address = address;
        }
        public void UpdateCustomerId(Guid customerId)
        {
            CustomerId = customerId;
        }

        public void UpdateTotalPrice(double totalPrice)
        {
            TotalPrice = totalPrice;
        }

        public void CalculateTotalPrice()
        {
            if(this.OrderItems!=null && this.OrderItems.Count > 0)
            {
                this.OrderItems.ForEach(x =>
                {
                    this.TotalPrice += x.TotalPrice;
                });
            }
            
        }
    }
}
