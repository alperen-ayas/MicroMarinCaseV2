using Azure.Core;
using MicroMarinCaseV2.Application.Filters;
using MicroMarinCaseV2.Application.Wrappers;
using System.Text.Json.Nodes;

namespace MicroMarinCaseV2.Api.Strategies
{
    public class Operation : IOperation
    {
        private IRecordStrategy _strategy;
        
        public Task<Result> Create(JsonObject request)
        {
            return this._strategy.CreateRequest(request);
        }

        public Task<Result> Delete(Guid id)
        {
            return this._strategy.DeleteRequest(id);
        }

        public Task<Result<object>> Get(FilterParameters filterParameters)
        {
            return this._strategy.GetRequest(filterParameters);
        }

        public void SetStrategy(IRecordStrategy strategy)
        {
            _strategy = strategy;
        }

        public Task<Result> Update(Guid id, JsonObject request)
        {
            return this._strategy.UpdateRequest(id,request);
        }
    }
}
