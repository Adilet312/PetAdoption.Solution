using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace PetAdoption.Controllers
{
    public class CustomersController:Controller
    {
        private readonly PetAdoptionContextDB _dataBase;
        public CustomersController(PetAdoptionContextDB db)
        {
            _dataBase=db;
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<Customer> customers = _dataBase.Customers.ToList();
            return View(customers);
        }
        [HttpGet]
        public ActionResult Read(int id)
        {
            var customerList = _dataBase.Customers
                               .Include(rowPets => rowPets.Pets)
                               .ThenInclude(join => join.Pet)
                               .FirstOrDefault(rowCustomers => rowCustomers.CustomerId==id);
                               return View(customerList); 
        }
        // [HttpGet]
        // public ActionResult Create()
        // {
        //     ViewBag.PetId = new SelectList(_dataBase.Pets,"PetId","Please Select Pet");
        //     return View();
        // }
        // [HttpPost]
        // public ActionResult Create(Customer new_customer, int PetId)
        // {
        //     _dataBase.Customers.Add(new_customer);
        //     if(PetId!=0)
        //     {
        //         _dataBase.CustomerPets.Add(new CustomerPet(){PetId = PetId,CustomerId=new_customer.CustomerId});
        //     }
        //     _dataBase.SaveChanges();
        //     return RedirectToAction("Index");
        // }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Customer new_customer)
        {
            System.Console.WriteLine(new_customer.CustPhone);
            _dataBase.Customers.Add(new_customer);
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int deleteID)
        {
            Customer deletingCustomer = _dataBase.Customers.FirstOrDefault(custs => custs.CustomerId==deleteID);
            return View(deletingCustomer);
        }
        [HttpPost]
        public ActionResult Update(Customer new_customer)
        {
            _dataBase.Entry(new_customer).State=EntityState.Modified;
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        }
        

    }
}