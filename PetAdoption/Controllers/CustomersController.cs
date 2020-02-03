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
      
        // Send Form for View or for browser.
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.PetId = new SelectList(_dataBase.Pets,"PetId","PetName");
            return View();
        }
        //Accept data that comes from form that user submited.
        [HttpPost]
        public ActionResult Create(Customer new_customer,int PetId)
        {
            _dataBase.Customers.Add(new_customer);
            if(PetId!=0)
            {
                _dataBase.CustomerPets.Add(new CustomerPet(){PetId=PetId,CustomerId=new_customer.CustomerId});
            }
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
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
        [HttpGet]
        public ActionResult Update(int updateID)
        {
            Customer deletingPet = _dataBase.Customers.FirstOrDefault(custs => custs.CustomerId==updateID);
            ViewBag.PetId = new SelectList(_dataBase.Pets,"PetId","PetName");
            return View(deletingPet);
        }
        [HttpPost]
        public ActionResult Update(Customer new_customer)
        {
            _dataBase.Entry(new_customer).State=EntityState.Modified;
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        }

         [HttpGet]
        public ActionResult Delete(int deleteID)
        {
            Customer deletingCustomer = _dataBase.Customers.FirstOrDefault(customers =>customers.CustomerId==deleteID);
            return View(deletingCustomer);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(int deleteID)
        {
            Customer deletingCustomer = _dataBase.Customers.FirstOrDefault(customers =>customers.CustomerId==deleteID);
            _dataBase.Remove(deletingCustomer);
            _dataBase.SaveChanges();
            return View("ConfirmedAboutDeletion");
        }
        

    }
}