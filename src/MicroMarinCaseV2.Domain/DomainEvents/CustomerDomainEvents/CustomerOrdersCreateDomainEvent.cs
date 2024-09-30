using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.SeedWorks;

namespace MicroMarinCaseV2.Domain.DomainEvents.CustomerDomainEvents
{
    public record CustomerOrdersCreateDomainEvent(List<Order> Orders) : IDomainEvent;
}
