using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipiecesWeb.Models;
using RecipiecesWeb.Services;

namespace RecipiecesWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Hodge Family Digital Heirloom";
            
            return View();
        }

        public IActionResult Contact()
        {
            //ViewData["Message"] = "";

            return View();
        }

     
        [Authorize(Roles = "Admins")]
        public IActionResult Users()
        {
            ViewData["Message"] = "Registered users";

            // TODO: finish him!
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
