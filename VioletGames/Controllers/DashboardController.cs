using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Data.Filters;
using VioletGames.Models;

namespace VioletGames.Controllers
{
    [PageUserLogin]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Dashboard";
            return View();
        }

        //Actions Admin

        //Actions Stand
        
    }
}
