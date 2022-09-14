using MovieRental.Models;
using MovieRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRental.Controllers
{
    public class CustomersController : Controller
    {
        //-----------------------------------------------------------------------
        // First we need DbContext and initialize it in constructor
        public ApplicationDbContext _context;
        public CustomersController() {
            _context = new ApplicationDbContext();
        }

        // DbContext is a disposable object. We need to properly dispose this DbContext object.
        // need to override Dispose() method of base Controller class
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //-----------------------------------------------------------------------------------


       public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes

            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerDbId = _context.Customers.Single(c=> c.Id == customer.Id);
                // auto mapper
                //tryUpdate

                customerDbId.Name = customer.Name;
                customerDbId.BirthDate = customer.BirthDate;
                customerDbId.MembershipTypeId = customer.MembershipTypeId;
                customerDbId.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        [Route("customers/details")]
        public ActionResult Details(int customerId)
        {
            //// Hard coded customer data done by me
            //var cList = new List<Customer> {
            //    new Customer() { Id = 1, Name = "John Smith"},
            //    new Customer() {Id = 2, Name = "Mary Williams"}
            //};

            //var customerName = cList.ElementAt(id - 1);

            // Customer data from Database
            var customerName = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == customerId); // Here query will be immidiately executed because of SingleOrDefault() method
            if (customerName == null)
                return HttpNotFound();
            
            return View(customerName);
        }


        //[Route("customer/index")]
        public ActionResult Index()
        {
            //// Hard coded customer data done by me
            //var cList = new List<Customer> {
            //    new Customer() { Id = 1, Name = "John Smith"},
            //    new Customer() {Id = 2, Name = "Mary Williams"}
            //};
            //var customerList = new CustomerListViewModel()
            //{
            //    Customers = cList
            //};

            // cusomer data from database
            var customerList = _context.Customers.Include(c => c.MembershipType);    // This Customer property is defined in DbSet in our DbContext
                                                       // It is a Deffed execution. Query is not going to execute immidiately. It will execute when iterate over this customerList object.
                                                       // If we use ToList() method then it will execute.


            return View(customerList);
        }


        //// hard coded customer data done by Mosh
        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer> {
        //        new Customer{Id=1, Name="John Smith"},
        //        new Customer{Id=2,Name="Mary Williams"}

        //    };
        //}

        public ActionResult Edit(int customerId)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == customerId);
            if (customer == null)
                HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            }; 
            return View("CustomerForm", viewModel);
        }
    }
}