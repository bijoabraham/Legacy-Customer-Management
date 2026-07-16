using CustomersWebDemo.DbAccess;
using CustomersWebDemo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CustomersWebDemo.Services;

namespace CustomersWebDemo.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ICustomerService _customerService;


        public CustomerController() : this(new CustomerService())
        {
        }

        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _customerService.Dispose();
            }

            base.Dispose(disposing);
        }



        public ActionResult Index()
        {
            return View();
        }


        public PartialViewResult Filter(string nameFilter, string emailFilter,
            string locationFilter, int? typeFilter, int page)
        {
            // take customers that are not deleted

            var data = _customerService.Filter(nameFilter, emailFilter, locationFilter, typeFilter, page);

            return PartialView(data);
        }

        public ActionResult Create()
        {
            ModelState.Clear();
            return View("CreateEdit", new Customer());
        }
        
        public ActionResult Edit(int id)
        {
            ModelState.Clear();

            var customer = _customerService.GetById(id);

            return View("CreateEdit", customer);
        }



        public ActionResult CreateEdit(Customer c)
        {
            if (ModelState.IsValid)
            {
                _customerService.Save(c);

                return RedirectToAction("Index");
            }

            return View(c);
        }




        public ActionResult Details(int id)
        {
            var customer = _customerService.GetById(id);

            return View(customer);
        }




        public ActionResult Delete(int id)
        {
            var customer = _customerService.GetById(id);

            return View(customer);
        }

       
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _customerService.Delete(id);

            return RedirectToAction("Index");
        }

    }
}