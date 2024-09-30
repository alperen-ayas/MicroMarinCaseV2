using MicroMarinCaseV2.Application.Filters;
using MicroMarinCaseV2.Application.Wrappers;
using System.Text.Json.Nodes;

namespace MicroMarinCaseV2.Api.Strategies
{
    public interface IRecordStrategy
    {
        public Task<Result> CreateRequest(JsonObject request);
        public Task<Result> DeleteRequest(Guid id);
        public Task<Result> UpdateRequest(Guid id, JsonObject request);
        public Task<Result<object>> GetRequest(FilterParameters filterParameters);
    }
}
