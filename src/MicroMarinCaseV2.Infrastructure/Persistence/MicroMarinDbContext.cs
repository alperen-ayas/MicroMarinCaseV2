using MediatR;
using MicroMarinCaseV2.Domain.AggregateModels.CustomerModels;
using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.AggregateModels.ProductModels;
using MicroMarinCaseV2.Domain.SeedWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCaseV2.Infrastructure.Persistence
{
    public class MicroMarinDbContext : DbContext
    {
        private readonly IMediator _mediator;
        public MicroMarinDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(x => x.Id);
            
            modelBuilder.Entity<Customer>().Property(x=>x.Email).IsRequired(false);
            modelBuilder.Entity<Customer>().Property(x=>x.Name).IsRequired();
            modelBuilder.Entity<Customer>().Property(x=>x.Surname).IsRequired();

            modelBuilder.Entity<Customer>().OwnsOne(x => x.Address);
            modelBuilder.Entity<Customer>().Ignore(x => x._domainEvents);

            modelBuilder.Entity<Customer>().HasMany(x=>x.Orders).WithOne(x=>x.Customer)
                .HasForeignKey(x=>x.CustomerId).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>().Property(x => x.CustomerId).IsRequired();
            modelBuilder.Entity<Order>().OwnsOne(x => x.Address);
            modelBuilder.Entity<Order>().Ignore(x => x._domainEvents);

            modelBuilder.Entity<Order>().HasMany(x => x.OrderItems).WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderItem>().HasKey(x => x.Id);
            modelBuilder.Entity<OrderItem>().Property(x => x.ProductId).IsRequired();
            modelBuilder.Entity<OrderItem>().Property(x => x.OrderId).IsRequired();
            modelBuilder.Entity<OrderItem>().Property(x => x.Count).IsRequired();

            modelBuilder.Entity<OrderItem>().HasOne(x => x.Product).WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderItem>().HasOne(x => x.Order).WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Product>().Property(x => x.BasePrice).IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Product>().Ignore(x => x._domainEvents);


            base.OnModelCreating(modelBuilder);
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var domainEntities = this.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.Entity._domainEvents != null && x.Entity._domainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity._domainEvents)
                .ToList();

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);


            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
