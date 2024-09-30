using MediatR;
using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.Dtos;
using MicroMarinCaseV2.Application.Wrappers;
using MicroMarinCaseV2.Domain.AggregateModels.CustomerModels;
using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.DomainEvents.CustomerDomainEvents;
using MicroMarinCaseV2.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.UseCases.CustomerUseCases.Commands
{
    public class CustomerCreateCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public List<OrderCreateDto>? Orders { get; set; }
    }

    public class CustomerCreateCommandHandler : IRequestHandler<CustomerCreateCommand, Result>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerCreateCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
        {
            Guid customerId = Guid.NewGuid();
            var customer = Customer.Create(customerId, request.Name, request.Surname, request.Email, request.Address);
            if(request.Orders != null && request.Orders.Count>0)
            {
                List<Order> orders = new List<Order>();

                request.Orders.ForEach(x =>
                {
                    Guid orderId = Guid.NewGuid();
                    var orderItems = new List<OrderItem>();

                    x.OrderItems.ForEach(z => {
                        orderItems.Add(OrderItem.Create(Guid.NewGuid(), z.Count, z.Price, z.ProductId, orderId));
                        });

                    orders.Add(Order.Create(orderId,request.Address,customerId,orderItems));

                });
                var @event = new CustomerOrdersCreateDomainEvent(orders);

                customer.AddDomainEvent(@event);
            }

            await _customerRepository.Create(customer);

            await _customerRepository.SaveChangesAsync(cancellationToken);

            return Result.Success("Customer created.");
        }
    }
}
