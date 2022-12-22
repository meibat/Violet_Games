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
    public class CaixaController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Caixa";
            return View();
        }
    }
}
