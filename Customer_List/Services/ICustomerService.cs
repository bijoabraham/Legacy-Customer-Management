using System;
using CustomersWebDemo.Models;

namespace CustomersWebDemo.Services
{
    public interface ICustomerService : IDisposable
    {
        object Filter(string nameFilter, string emailFilter,
            string locationFilter, int? typeFilter, int page);
        object GetById(int id);
        void Save(Customer c);
        object GetById(int id);
        object GetById(int id);
        void Delete(int id);
    }
}
