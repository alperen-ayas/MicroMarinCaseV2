using Mapster;
using MediatR;
using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.Dtos.QueryDtos;
using MicroMarinCaseV2.Application.Filters;
using MicroMarinCaseV2.Application.UseCases.OrderUseCases.Queries;
using MicroMarinCaseV2.Application.Wrappers;
using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.UseCases.ProductUseCases.Queries
{
    public class ProductGetQuery : IRequest<Result<object>>
    {
        public FilterParameters FilterParameters { get; set; }
    }
    public class ProductGetQueryHandler : IRequestHandler<ProductGetQuery, Result<object>>
    {
        private readonly IProductRepository _repository;

        public ProductGetQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<object>> Handle(ProductGetQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAll(request.FilterParameters);
            var mappedDto = products.Adapt<List<ProductQueryDto>>();
            return Result<object>.Success(mappedDto, "Porducts");
        }
    }
}
