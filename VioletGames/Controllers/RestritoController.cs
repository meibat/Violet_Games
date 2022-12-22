using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VioletGames.Controllers
{
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            TempData["MessagemError"] = "Ops, seu usuário não tem permissão para acessar essa página.";
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
