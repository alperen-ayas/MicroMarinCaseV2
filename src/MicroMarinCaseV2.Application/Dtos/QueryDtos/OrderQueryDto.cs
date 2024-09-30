using MicroMarinCaseV2.Domain.AggregateModels.CustomerModels;
using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.Dtos.QueryDtos
{
    public class OrderQueryDto
    {
        public Guid Id { get; set; }
        public double TotalPrice { get; set; }
        public Address Address { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderItemQueryDto> OrderItems { get; set; }
    }
}
