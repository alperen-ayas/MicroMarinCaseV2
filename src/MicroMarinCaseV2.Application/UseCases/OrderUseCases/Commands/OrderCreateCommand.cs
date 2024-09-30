using MediatR;
using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.Dtos;
using MicroMarinCaseV2.Application.Wrappers;
using MicroMarinCaseV2.Domain.AggregateModels.CustomerModels;
using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.UseCases.OrderUseCases.Commands
{
    public class OrderCreateCommand : IRequest<Result>
    {
        public Address Address { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderItemCreateDto> OrderItems { get; set; }
    }

    public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderCreateCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            Guid orderId = Guid.NewGuid();
            List<OrderItem> orderItems = new List<OrderItem>();
            request.OrderItems.ForEach(x =>
            {
                orderItems.Add(OrderItem.Create(Guid.NewGuid(), x.Count, x.Price, x.ProductId, orderId));
            });
            await _orderRepository.Create(Order.Create(orderId, request.Address, request.CustomerId, orderItems));
            await _orderRepository.SaveChangesAsync(cancellationToken);

            return Result.Success("Order Created");
        }
    }
}
