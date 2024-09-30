using Azure.Core;
using MicroMarinCaseV2.Application.Abstaractions.Repositories;
using MicroMarinCaseV2.Application.Filters;
using MicroMarinCaseV2.Domain.AggregateModels.CustomerModels;
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
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly MicroMarinDbContext _context;

        public CustomerRepository(MicroMarinDbContext context)
        {
            _context = context;
        }

        public async Task Create(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public async Task<Customer> Get(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<List<Customer>> GetAll(FilterParameters filterParameters)
        {
            var query = _context.Customers.AsNoTracking();

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
                    query = query.Where(ExpressionTool.CreateExpression<Customer>(filter.Key, filter.Value, ExpressionType.Equal));
                }
                else if (filter.FilterType == "GreaterThan")
                {
                    query = query.Where((ExpressionTool.CreateExpression<Customer>(filter.Key, filter.Value, ExpressionType.GreaterThan)));
                }
                else if (filter.FilterType == "LessThan")
                {
                    query = query.Where((ExpressionTool.CreateExpression<Customer>(filter.Key, filter.Value, ExpressionType.LessThan)));
                }
            }

            return await query.ToListAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Customer customer)
        {
            _context.Customers.Update(customer);
        }
    }
}
