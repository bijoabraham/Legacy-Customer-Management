using System;
using System.Linq;
using CustomersWebDemo.Models;

namespace CustomersWebDemo.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerEntitiesDbContext _db;
        private readonly bool _ownsDbContext;

        public CustomerService()
        {
            this._db = new CustomerEntitiesDbContext();
            this._ownsDbContext = true;
        }

        public CustomerService(CustomerEntitiesDbContext dbContext)
        {
            this._db = dbContext;
            this._ownsDbContext = false;
        }

        public void Dispose()
        {
            if (_ownsDbContext)
            {
                _db.Dispose();
            }
        }

        public object Filter(string nameFilter, string emailFilter,
                    string locationFilter, int? typeFilter, int page)
        {
            var query = _db.Customers.Where(f => f.FlagDeleted == 0);

            // аpply filters

            if (!string.IsNullOrEmpty(nameFilter))
            {
                query = query.Where(f => f.CustomerName.Contains(nameFilter));
            }

            if (!string.IsNullOrEmpty(emailFilter))
            {
                query = query.Where(f => f.Email.Contains(emailFilter));
            }

            if (!string.IsNullOrEmpty(locationFilter))
            {
                query = query.Where(f => f.Location.Contains(locationFilter));
            }

            if (typeFilter != null)
            {
                query = query.Where(f => f.CustomerType == (CustomerTypeEnum)typeFilter);
            }

            // get page and count info

            int pageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["pageSize"]);

            int itemCount = query.Count();

            // return 1 page of filtered data

            var data = query.OrderBy(a => a.CustomerID)
            .Skip(page * pageSize).Take(pageSize).ToList();

            // populate view data variables

            ViewData["itemCount"] = itemCount;

            ViewData["currentPage"] = page;

            return data;
        }

        public object GetById(int id)
        {
            return _db.Customers.Find(id);
        }

        public void Save(Customer c)
        {
            if (c.CustomerID <= 0)   // new customer
            {
                _db.Customers.Add(c);
            }
            else // existing customer
            {
                _db.Entry(c).State = EntityState.Modified;
            }

            _db.SaveChanges();
        }

        public object GetById(int id)
        {
            var customer = _db.Customers.Find(id);

            return customer;
        }

        public object GetById(int id)
        {
            var customer = _db.Customers.Find(id);

            return customer;
        }

        public void Delete(int id)
        {
            var customer = _db.Customers.Find(id);

            customer.FlagDeleted = 1;

            _db.SaveChanges();
        }
    }
}
