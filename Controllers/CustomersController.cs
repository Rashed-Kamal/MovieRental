using MovieRental.Models;
using MovieRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRental.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Manage()
        {
            var customer = new Customer() { Id = 1, Name = "John" };
            return View(customer);
        }

        [Route("customers/details")]
        public ActionResult Details(int id)
        {
            var cList = new List<Customer> {
                new Customer() { Id = 1, Name = "John Smith"},
                new Customer() {Id = 2, Name = "Mary Williams"}
            };
            //var customerList = new CustomerListViewModel()
            //{
            //    Customers = cList
            //};

            var customerName = cList.ElementAt(id - 1);
            return View(customerName);
        }


        //[Route("customer/index")]
        public ActionResult Index()
        {
            var cList = new List<Customer> {
                new Customer() { Id = 1, Name = "John Smith"},
                new Customer() {Id = 2, Name = "Mary Williams"}
            };
            var customerList = new CustomerListViewModel()
            {
                Customers = cList
            };
            return View(customerList);
        }
    }
}