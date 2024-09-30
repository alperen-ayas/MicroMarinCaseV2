using MediatR;
using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.UseCases.ProductUseCases.Commands
{
    public class ProductUpdateCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
    }

    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public ProductUpdateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.Id);

            product.Update(request.Name, request.BasePrice);

            await _productRepository.SaveChangesAsync(cancellationToken);

            return Result.Success("Product updated.");
        }
    }
}
