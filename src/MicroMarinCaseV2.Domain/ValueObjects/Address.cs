using MicroMarinCaseV2.Domain.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Domain.ValueObjects
{
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
        public string AddressLine2 { get; set; }
        public Address()
        {
            
        }
        public Address(string country,string city,string addressLine)
        {
            Country = country;
            City = city;
            AddressLine = addressLine;
        }
    }
}
