using MicroMarinCaseV2.Application.Filters;
using MicroMarinCaseV2.Domain.AggregateModels.CustomerModels;
using MicroMarinCaseV2.Domain.SeedWorks;

namespace MicroMarinCaseV2.Application.Abstaractions.Repositories
{
    public interface ICustomerRepository : IUnitOfWork
    {
        Task Create(Customer customer);
        Task Delete(Customer customer);
        Task<Customer> Get(Guid id);
        Task<List<Customer>> GetAll(FilterParameters filterParameters);
    }
}
