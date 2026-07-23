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
        public CustomerEntitiesDbContext() : base("CustomersDB") { }

            public DbSet<Customer> Customers { get; set; }
        
            public virtual void Commit()
            {
                this.SaveChanges();
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Configurations.Add(new CustomerConfiguration());
                
            }
        
    }
}