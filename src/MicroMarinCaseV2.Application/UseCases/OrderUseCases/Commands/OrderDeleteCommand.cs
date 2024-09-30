using MediatR;
using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.UseCases.CustomerUseCases.Commands;
using MicroMarinCaseV2.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.UseCases.OrderUseCases.Commands
{
    public class OrderDeleteCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    public class OrderDeleteCommandHandler : IRequestHandler<OrderDeleteCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderDeleteCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result> Handle(OrderDeleteCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.Get(request.Id);

            await _orderRepository.Delete(order);

            await _orderRepository.SaveChangesAsync(cancellationToken);

            return Result.Success("Order deleted.");
        }
    }
}
