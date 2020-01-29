using Microsoft.AspNetCore.Mvc;
using PetAdoption.Models;

namespace  PetAdoption.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}