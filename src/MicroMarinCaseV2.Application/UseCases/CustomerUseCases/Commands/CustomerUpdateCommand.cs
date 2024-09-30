using MediatR;
using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.Wrappers;
using MicroMarinCaseV2.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.UseCases.CustomerUseCases.Commands
{
    public class CustomerUpdateCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }

    public class CustomerUpdateCommandHandler : IRequestHandler<CustomerUpdateCommand, Result>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerUpdateCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.Get(request.Id);
            customer.Update(request.Name,request.Surname,request.Email,request.Address);

            await _customerRepository.SaveChangesAsync(cancellationToken);

            return Result.Success("Customer updated.");
        }
    }
}
