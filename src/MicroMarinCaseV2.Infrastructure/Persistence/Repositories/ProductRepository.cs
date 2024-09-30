using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.Filters;
using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.AggregateModels.ProductModels;
using MicroMarinCaseV2.Infrastructure.ExpressionCommon;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MicroMarinDbContext _context;

        public ProductRepository(MicroMarinDbContext context)
        {
            _context = context;
        }

        public async Task Create(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<Product> Get(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetAll(FilterParameters filterParameters)
        {
            var query = _context.Products.AsNoTracking();

            if (filterParameters.Filters == null)
                return await query.ToListAsync();

            foreach (var filter in filterParameters.Filters)
            {
                if (filter.FilterType == "includes")
                {
                    query = query.Include(filter.Key);
                }
                else if (filter.FilterType == "Equals")
                {
                    query = query.Where(ExpressionTool.CreateExpression<Product>(filter.Key, filter.Value, ExpressionType.Equal));
                }
                else if (filter.FilterType == "GreaterThan")
                {
                    query = query.Where((ExpressionTool.CreateExpression<Product>(filter.Key, filter.Value, ExpressionType.GreaterThan)));
                }
                else if (filter.FilterType == "LessThan")
                {
                    query = query.Where((ExpressionTool.CreateExpression<Product>(filter.Key, filter.Value, ExpressionType.LessThan)));
                }
            }

            return await query.ToListAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
