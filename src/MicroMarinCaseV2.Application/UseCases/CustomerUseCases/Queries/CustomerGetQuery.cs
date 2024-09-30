using Mapster;
using MediatR;
using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.Dtos.QueryDtos;
using MicroMarinCaseV2.Application.Filters;
using MicroMarinCaseV2.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.UseCases.CustomerUseCases.Queries
{
    public class CustomerGetQuery : IRequest<Result<object>>
    {
        public FilterParameters FilterParameters { get; set; }
    }
    public class CustomerGetQueryHandler : IRequestHandler<CustomerGetQuery, Result<object>>
    {
        private readonly ICustomerRepository _repository;

        public CustomerGetQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<object>> Handle(CustomerGetQuery request, CancellationToken cancellationToken)
        {
            var customers = await _repository.GetAll(request.FilterParameters);
            var mappedDto = customers.Adapt<List<CustomerQueryDto>>();
            return Result<object>.Success(mappedDto, "Customers");
        }
    }
}
