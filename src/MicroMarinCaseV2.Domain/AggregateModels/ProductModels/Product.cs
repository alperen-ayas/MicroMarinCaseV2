using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Domain.AggregateModels.ProductModels
{
    public class Product : AggregateRoot
    {
        public override Guid Id { get; set ; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        private Product(Guid id, string name, decimal basePrice)
        {
            Id = id;
            Name = name;
            BasePrice = basePrice;
        }
        protected Product()
        {
            
        }

        public static Product Create(Guid id, string name, decimal basePrice)
        {
            return new(id, name, basePrice);
        }

        public void Update(string name, decimal basePrice)
        {
            Name = name;
            BasePrice = basePrice;
        }
    }
}
