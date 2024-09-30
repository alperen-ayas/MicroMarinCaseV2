using MediatR;
using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.UseCases.CustomerUseCases.Commands;
using MicroMarinCaseV2.Application.Wrappers;
using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.UseCases.OrderUseCases.Commands
{
    public class OrderUpdateCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public Address Address { get; set; }
        public Guid CustomerId { get; set; }
    }

    public class OrderUpdateCommandHandler : IRequestHandler<OrderUpdateCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderUpdateCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.Get(request.Id);
            order.UpdateAddress(request.Address);
            order.UpdateCustomerId(request.CustomerId);

            await _orderRepository.SaveChangesAsync(cancellationToken);

            return Result.Success("Order updated.");
        }
    }
}
