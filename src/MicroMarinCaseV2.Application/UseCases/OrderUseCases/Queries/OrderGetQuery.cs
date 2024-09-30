using Mapster;
using MediatR;
using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.Dtos.QueryDtos;
using MicroMarinCaseV2.Application.Filters;
using MicroMarinCaseV2.Application.UseCases.CustomerUseCases.Queries;
using MicroMarinCaseV2.Application.Wrappers;
using MicroMarinCaseV2.Domain.AggregateModels.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.UseCases.OrderUseCases.Queries
{
    public class OrderGetQuery : IRequest<Result<object>>
    {
        public FilterParameters FilterParameters { get; set; }
    }
    public class OrderGetQueryHandler : IRequestHandler<OrderGetQuery, Result<object>>
    {
        private readonly IOrderRepository _repository;

        public OrderGetQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<object>> Handle(OrderGetQuery request, CancellationToken cancellationToken)
        {
            var orders = await _repository.GetAll(request.FilterParameters);
            var mappedDto = orders.Adapt<List<OrderQueryDto>>();
            return Result<object>.Success(mappedDto, "Orders");
        }
    }
}
