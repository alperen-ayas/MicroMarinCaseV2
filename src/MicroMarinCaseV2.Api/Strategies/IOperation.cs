using MicroMarinCaseV2.Application.Filters;
using MicroMarinCaseV2.Application.Wrappers;
using System.Text.Json.Nodes;

namespace MicroMarinCaseV2.Api.Strategies
{
    public interface IOperation
    {
        public Task<Result> Create(JsonObject request);
        public Task<Result> Update(Guid id, JsonObject request);
        public Task<Result> Delete(Guid id);
        public Task<Result<object>> Get(FilterParameters filterParameters);
        public void SetStrategy(IRecordStrategy strategy);
    }
}
