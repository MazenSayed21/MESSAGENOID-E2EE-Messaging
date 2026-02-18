using System.Diagnostics;
using MESSAGENOID.Models;
using Microsoft.AspNetCore.Mvc;

namespace MESSAGENOID.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
