using MicroMarinCaseV2.Application.Filters;
using MicroMarinCaseV2.Domain.AggregateModels.CustomerModels;
using MicroMarinCaseV2.Domain.AggregateModels.ProductModels;
using MicroMarinCaseV2.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application.Abstaractions.Repositories
{
    public interface IProductRepository : IUnitOfWork
    {
        Task Create(Product product);
        Task Delete(Product product);
        Task<Product> Get(Guid id);
        Task<List<Product>> GetAll(FilterParameters filterParameters);
    }
}
