using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
namespace PetAdoption.Controllers
{
    [Authorize]
    public class PetsController:Controller
    {
        private readonly PetAdoptionContextDB _dataBase;
        private readonly UserManager<ApplicationUser> _userManager;
        public PetsController(PetAdoptionContextDB db,UserManager<ApplicationUser> userManager)
        {
            _dataBase = db;
            _userManager = userManager;
        }
        public async Task<ActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var userpets = _dataBase.Pets.Where(entry => entry.User.Id == currentUser.Id);
            return View(userpets);
        }
        [HttpGet]
        public ActionResult Read(int readID)
        {
            var petList = _dataBase.Pets
                               .Include(pets => pets.Customers)
                               .ThenInclude(join => join.Customer)
                               .FirstOrDefault(pets => pets.PetId==readID);
                               return View(petList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(_dataBase.Customers,"CustomerId","CustName");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Pet pet, int CustomerId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            pet.User = currentUser;
            _dataBase.Pets.Add(pet);
            if (CustomerId != 0)
            {
                _dataBase.CustomerPets.Add(new CustomerPet() { CustomerId = CustomerId, PetId = pet.PetId });
            }
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        } 
        [HttpGet]
        public ActionResult Update(int updateID)
        {
            Pet updatePet = _dataBase.Pets.FirstOrDefault(rowPets => rowPets.PetId==updateID);
            ViewBag.CustomerId = new SelectList(_dataBase.Customers,"CustomerId","CustName");
            return View(updatePet);
        }
        [HttpPost]
        public ActionResult Update(Pet update_Pet,int CustomerId)
        {
            if(CustomerId!=0)
            {
                _dataBase.CustomerPets.Add(new CustomerPet(){CustomerId=CustomerId, PetId=update_Pet.PetId});
            }
            _dataBase.Entry(update_Pet).State = EntityState.Modified;
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AddCustomer(int id)
        {
            
            var thisPet = _dataBase.Pets.FirstOrDefault(pets => pets.PetId==id);
            ViewBag.CustomerId = new SelectList(_dataBase.Customers,"CustomerId","CustName");
            return View(thisPet);
        }
        
        [HttpPost]
        public ActionResult AddCustomer(Pet pets, int CustomerId)
        {
            if (CustomerId != 0)
            {
                _dataBase.CustomerPets.Add(new CustomerPet(){CustomerId=CustomerId,PetId=pets.PetId});
            
            }
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int deleteID)
        {
            Pet deletingPet = _dataBase.Pets.FirstOrDefault(pets =>pets.PetId==deleteID);
            return View(deletingPet);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(int deleteID)
        {
            Pet deletingPet = _dataBase.Pets.FirstOrDefault(pets =>pets.PetId==deleteID);
            _dataBase.Remove(deletingPet);
            _dataBase.SaveChanges();
            return View("Index");
        }
        [HttpPost]
        public ActionResult DeleteCustomer(int joinId)
        {
            var joinEntry = _dataBase.CustomerPets.FirstOrDefault(entry => entry.CustomerPetId == joinId);
            _dataBase.CustomerPets.Remove(joinEntry);
            _dataBase.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}