using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.AggregateModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.Dtos.QueryDtos
{
    public class OrderItemQueryDto
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
    }
}
