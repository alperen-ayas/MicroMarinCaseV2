using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.Filters;
using MicroMarinCaseV2.Domain.AggregateModels.CustomerModels;
using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
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
    internal class OrderRepository : IOrderRepository
    {
        private readonly MicroMarinDbContext _context;

        public OrderRepository(MicroMarinDbContext context)
        {
            _context = context;
        }

        public async Task Create(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task CreateRange(List<Order> orders)
        {
            await _context.Orders.AddRangeAsync(orders);
        }

        public async Task Delete(Order order)
        {
            _context.Orders.Remove(order);
        }

        public async Task<Order> Get(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<List<Order>> GetAll(FilterParameters filterParameters)
        {
            var query = _context.Orders.AsNoTracking();

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
                    query = query.Where(ExpressionTool.CreateExpression<Order>(filter.Key, filter.Value, ExpressionType.Equal));
                }
                else if (filter.FilterType == "GreaterThan")
                {
                    query = query.Where((ExpressionTool.CreateExpression<Order>(filter.Key, filter.Value, ExpressionType.GreaterThan)));
                }
                else if (filter.FilterType == "LessThan")
                {
                    query = query.Where((ExpressionTool.CreateExpression<Order>(filter.Key, filter.Value, ExpressionType.LessThan)));
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
