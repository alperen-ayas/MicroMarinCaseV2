using MediatR;
using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.Wrappers;
using MicroMarinCaseV2.Domain.AggregateModels.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.UseCases.ProductUseCases.Commands
{
    public class ProductCreateCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
    }

    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public ProductCreateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.Create(Product.Create(Guid.NewGuid(), request.Name, request.BasePrice));

            await _productRepository.SaveChangesAsync(cancellationToken);

            return Result.Success("Product created");
        }
    }
}
