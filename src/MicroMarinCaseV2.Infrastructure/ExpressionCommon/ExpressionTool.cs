using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Infrastructure.ExpressionCommon
{
    public static class ExpressionTool
    {
        public static Expression<Func<T, bool>> CreateExpression<T>(string propertyName, object value, ExpressionType comparisonType)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);

            var convertedValue = Expression.Constant(Convert.ChangeType(value, property.Type));

            Expression comparison = comparisonType switch
            {
                ExpressionType.Equal => Expression.Equal(property, convertedValue),
                ExpressionType.GreaterThan => Expression.GreaterThan(property, convertedValue),
                ExpressionType.LessThan => Expression.LessThan(property, convertedValue),
                _ => throw new NotImplementedException("Supported comparison types are Equal, GreaterThan, and LessThan.")
            };

            return Expression.Lambda<Func<T, bool>>(comparison, parameter);
        }

        
    }
}
