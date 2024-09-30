using MicroMarinCaseV2.Domain.AggregateModels.CustomerModels;
using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.Dtos
{
    public class OrderCreateDto
    {
        public Address Address { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderItemCreateDto> OrderItems { get; set; }
    }
}
