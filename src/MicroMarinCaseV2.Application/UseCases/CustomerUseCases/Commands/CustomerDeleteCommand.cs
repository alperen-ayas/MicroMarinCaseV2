using MediatR;
using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.UseCases.CustomerUseCases.Commands
{
    public class CustomerDeleteCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    public class CustomerDeleteCommandHandler : IRequestHandler<CustomerDeleteCommand, Result>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerDeleteCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result> Handle(CustomerDeleteCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.Get(request.Id);

            await _customerRepository.Delete(customer);

            await _customerRepository.SaveChangesAsync(cancellationToken);

            return Result.Success("Customer deleted.");
        }
    }
}
