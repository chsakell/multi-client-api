using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapingAPI.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Albums()
        {
            return View();
        }

        public IActionResult Artists()
        {
            return View();
        }

        public IActionResult Customers()
        {
            return View();
        }
    }
}
