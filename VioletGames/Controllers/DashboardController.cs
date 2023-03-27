using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Data.Filters;
using VioletGames.Data.Repositorio;
using VioletGames.Models;
using VioletGames.Repositorio;

namespace VioletGames.Controllers
{
    [PageUserLogin]
    public class DashboardController : Controller
    {
        private readonly IConsoleRepositorio _consoleRepositorio;

        public DashboardController(IConsoleRepositorio consoleRepositorio)
        {
            _consoleRepositorio = consoleRepositorio;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Dashboard";

            string sessionUser = HttpContext.Session.GetString("SessionSinginUser");

            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessionUser);

            return View(usuario);
        }

        //Actions Admin

        //Actions Stand


    }
}
