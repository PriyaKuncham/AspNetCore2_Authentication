using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEBAPPSample.Models;
using WEBAPPSample.Repo;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace WEBAPPSample.Controllers
{
    [Authorize(Policy = "Member")]
    public class HomeController : Controller
    {
        public HomeController(IConfiguration configuration)
        {
            WEBAPPSample.Repo.BaseRepo b = new BaseRepo(configuration);

        }
        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
