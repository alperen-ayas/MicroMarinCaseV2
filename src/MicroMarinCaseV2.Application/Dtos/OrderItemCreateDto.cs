using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.AggregateModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.Dtos
{
    public class OrderItemCreateDto
    {

        public int Count { get; set; }
        public double Price { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
    }
}
