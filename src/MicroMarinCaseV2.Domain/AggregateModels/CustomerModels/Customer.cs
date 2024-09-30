using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.SeedWorks;
using MicroMarinCaseV2.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Domain.AggregateModels.CustomerModels
{
    public class Customer : AggregateRoot
    {
        public override Guid Id { get; set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public Address Address { get; private set; }
        public List<Order> Orders { get; private set; }

        public Customer(Guid id, string name, string surname, string email, Address address)
        {
            Guard.CannotNull(id, nameof(id));
            Id = id;
            Guard.CannotNull(name, nameof(name));
            Name = name;
            Guard.CannotNull(surname, nameof(surname));
            Surname = surname;
            Guard.CannotNull(email, nameof(email));
            Email = email;
            Guard.CannotNull(address, nameof(address));
            Address = address;
            Orders = new List<Order>();
        }

        // For Ef Core
        protected Customer()
        {
            
        }
        

        public static Customer Create(Guid id, string name, string surname, string email, Address address)
        {
            return new(id,name, surname, email, address);
        }

        public void Update(string name, string surname, string email, Address address)
        {
            Guard.CannotNull(name, nameof(name));
            Name = name;
            Guard.CannotNull(surname, nameof(surname));
            Surname = surname;
            Guard.CannotNull(email, nameof(email));
            Email = email;
            Guard.CannotNull(address, nameof(address));
            Address = address;
        }

        public void AddOrders(List<Order> orders)
        {
            Guard.CannotNull(orders, nameof(orders));
            Orders.AddRange(orders);
        }
    }
}
