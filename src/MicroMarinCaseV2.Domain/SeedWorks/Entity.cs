using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Domain.SeedWorks
{
    public abstract class Entity : IEntity
    {
        public abstract Guid Id { get; set; }
    }


}
