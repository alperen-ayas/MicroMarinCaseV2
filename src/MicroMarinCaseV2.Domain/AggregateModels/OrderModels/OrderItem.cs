using MicroMarinCaseV2.Domain.AggregateModels.ProductModels;
using MicroMarinCaseV2.Domain.SeedWorks;
using MicroMarinCaseV2.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Domain.AggregateModels.OrderModels
{
    public class OrderItem : Entity
    {
        public override Guid Id { get ; set; }

        public int Count { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get
            {
                return Price * Count;
            }}


        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Order Order { get; set; }
        public Guid OrderId { get; set; }

        private OrderItem(Guid id, int count, double price, Guid productId, Guid orderId)
        {
            Id = id;
            Count = count;
            Price = price;
            ProductId = productId;
            OrderId = orderId;
        }
        protected OrderItem()
        {

        }

        public static OrderItem Create(Guid id, int count, double price, Guid productId, Guid orderId)
        {
            return new(id, count, price, productId, orderId);
        }

        public void Update(int count, double price, Guid productId, Guid orderId)
        {
            Count = count;
            Price = price;
            ProductId = productId;
            OrderId = orderId;
        }
    }
}
