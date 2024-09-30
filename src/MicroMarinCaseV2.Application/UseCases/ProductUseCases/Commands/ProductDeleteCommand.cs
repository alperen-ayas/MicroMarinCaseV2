using MediatR;
using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.UseCases.OrderUseCases.Commands;
using MicroMarinCaseV2.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.UseCases.ProductUseCases.Commands
{
    public class ProductDeleteCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand, Result>
    {
        private readonly IProductRepository _productRepository;

        public ProductDeleteCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.Id);

            await _productRepository.Delete(product);

            await _productRepository.SaveChangesAsync(cancellationToken);

            return Result.Success("Product deleted.");
        }
    }
}
