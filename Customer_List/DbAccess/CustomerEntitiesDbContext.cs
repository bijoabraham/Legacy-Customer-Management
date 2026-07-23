using CustomersWebDemo.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web;

namespace CustomersWebDemo.DbAccess
{
    public class CustomerEntitiesDbContext : DbContext
    {
        public CustomerEntitiesDbContext(DbContextOptions<CustomerEntitiesDbContext> options) : base(options) { }

            public DbSet<Customer> Customers { get; set; }
        
            public virtual void Commit()
            {
                base.SaveChanges();
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.ApplyConfiguration(new CustomerConfiguration());
                
            }
        
    }
}