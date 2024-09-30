﻿using Mapster;
using MediatR;
using MicroMarinCaseV2.Application.Filters;
using MicroMarinCaseV2.Application.UseCases.CustomerUseCases.Commands;
using MicroMarinCaseV2.Application.UseCases.CustomerUseCases.Queries;
using MicroMarinCaseV2.Application.UseCases.OrderUseCases.Commands;
using MicroMarinCaseV2.Application.UseCases.OrderUseCases.Queries;
using MicroMarinCaseV2.Application.Wrappers;
using System.Text.Json.Nodes;

namespace MicroMarinCaseV2.Api.Strategies
{
    public class OrderStrategy : IRecordStrategy
    {
        private readonly IMediator _mediator;

        public OrderStrategy(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Result> CreateRequest(JsonObject request)
        {
            var command = request.Adapt<OrderCreateCommand>();

            return await _mediator.Send(command);
        }

        public async Task<Result> DeleteRequest(Guid id)
        {
            var command = new OrderDeleteCommand() { Id = id };

            return await _mediator.Send(command);
        }

        public async Task<Result<object>> GetRequest(FilterParameters filterParameters)
        {
            var query = new OrderGetQuery() { FilterParameters = filterParameters };

            return await _mediator.Send(query);
        }

        public async Task<Result> UpdateRequest(Guid id, JsonObject request)
        {
            var command = request.Adapt<OrderUpdateCommand>();

            command.Id = id;

            return await _mediator.Send(command);
        }
    }
}
