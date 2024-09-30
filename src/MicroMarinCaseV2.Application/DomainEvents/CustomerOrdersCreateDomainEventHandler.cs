using FluentValidation;
using MediatR;
using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.DomainEvents.CustomerDomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.DomainEvents
{
    internal class CustomerOrdersCreateDomainEventValidator : AbstractValidator<CustomerOrdersCreateDomainEvent>
    {
        public CustomerOrdersCreateDomainEventValidator()
        {
        }
    }
    internal class CustomerOrdersCreateDomainEventHandler : INotificationHandler<CustomerOrdersCreateDomainEvent>
    {
        private readonly IOrderRepository _orderRepository;

        public CustomerOrdersCreateDomainEventHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(CustomerOrdersCreateDomainEvent notification, CancellationToken cancellationToken)
        {
            List<Order> orders = new List<Order>();
            Guid orderId = Guid.NewGuid();

            notification.Orders.ForEach(o =>
            {
                var order = Order.Create(orderId, o.Address, o.CustomerId, o.OrderItems);
                orders.Add(order);
            });

            await _orderRepository.CreateRange(orders);

        }
    }
}
