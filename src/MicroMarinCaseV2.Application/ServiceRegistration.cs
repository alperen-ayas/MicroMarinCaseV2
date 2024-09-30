using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using MicroMarinCaseV2.Application.Behaviors;
using MicroMarinCaseV2.Application.Dtos;
using MicroMarinCaseV2.Application.UseCases.CustomerUseCases.Commands;
using MicroMarinCaseV2.Application.UseCases.OrderUseCases.Commands;
using MicroMarinCaseV2.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServies(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            var config = TypeAdapterConfig.GlobalSettings;
            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            TypeAdapterConfig<JsonObject, CustomerCreateCommand>
                .NewConfig()
                .AfterMapping((src, dest) =>
                    {
                        dest.Address = JsonSerializer.Deserialize<Address>(src["address"].ToString());

                        var ordersJson = src["orders"]?.ToString();
                        if (!string.IsNullOrWhiteSpace(ordersJson))
                        {
                            dest.Orders = JsonSerializer.Deserialize<List<OrderCreateDto>>(ordersJson);

                            for (int i = 0; i < dest.Orders.Count; i++)
                            {
                                var orderItemsJson = src["orders"][i]["orderItems"].ToString();
                                dest.Orders[i].OrderItems = JsonSerializer.Deserialize<List<OrderItemCreateDto>>(orderItemsJson);
                            }
                        }
                        
                    });


            TypeAdapterConfig<JsonObject, CustomerUpdateCommand>
                .NewConfig()
                .AfterMapping((src, dest) =>
                {
                    dest.Address = JsonSerializer.Deserialize<Address>(src["address"].ToString());

                });

            TypeAdapterConfig<JsonObject, OrderCreateCommand>
                .NewConfig()
                .AfterMapping((src, dest) =>
                {
                    dest.Address = JsonSerializer.Deserialize<Address>(src["address"].ToString());
                });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
