using MicroMarinCaseV2.Application.Filters;
using MicroMarinCaseV2.Domain.AggregateModels.CustomerModels;
using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.SeedWorks;

namespace MicroMarinCaseV2.Application.Abstaractions.Repositories
{
    public interface IOrderRepository: IUnitOfWork
    {
        Task CreateRange(List<Order> orders);
        Task Create(Order order);
        Task Delete(Order order);
        Task<Order> Get(Guid id);
        Task<List<Order>> GetAll(FilterParameters filterParameters);
    }
}
