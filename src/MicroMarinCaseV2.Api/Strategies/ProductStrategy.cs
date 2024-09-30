using Mapster;
using MediatR;
using MicroMarinCaseV2.Application.Filters;
using MicroMarinCaseV2.Application.UseCases.CustomerUseCases.Queries;
using MicroMarinCaseV2.Application.UseCases.ProductUseCases.Commands;
using MicroMarinCaseV2.Application.UseCases.ProductUseCases.Queries;
using MicroMarinCaseV2.Application.Wrappers;
using System.Text.Json.Nodes;

namespace MicroMarinCaseV2.Api.Strategies
{
    public class ProductStrategy : IRecordStrategy
    {
        private readonly IMediator _mediator;

        public ProductStrategy(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result> CreateRequest(JsonObject request)
        {
            var command = request.Adapt<ProductCreateCommand>();

            return await _mediator.Send(command);
        }

        public async Task<Result> DeleteRequest(Guid id)
        {
            var command = new ProductDeleteCommand() { Id = id };

            return await _mediator.Send(command);
        }

        public async Task<Result<object>> GetRequest(FilterParameters filterParameters)
        {
            var query = new ProductGetQuery() { FilterParameters = filterParameters };

            return await _mediator.Send(query);
        }

        public async Task<Result> UpdateRequest(Guid id, JsonObject request)
        {
            var command = request.Adapt<ProductUpdateCommand>();

            command.Id = id;

            return await _mediator.Send(command);
        }
    }
}
