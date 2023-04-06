using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VioletGames.Data.Filters;
using VioletGames.Data.Repositorio;
using VioletGames.Models;

namespace VioletGames.Controllers
{
    [PageUserLogin]
    public class DashboardController : Controller
    {
        private readonly IConsoleRepositorio _consoleRepositorio;
        private readonly IAgendamentoRepositorio _agendaRepositorio;

        public DashboardController(IConsoleRepositorio consoleRepositorio, IAgendamentoRepositorio agendaRepositorio)
        {
            _consoleRepositorio = consoleRepositorio;
            _agendaRepositorio = agendaRepositorio;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Dashboard";

            List<ConsoleModel> consoles = _consoleRepositorio.SearchAll();
            return View(consoles);
        }

       public IActionResult ConsoleDetail(int id)
        {
            ViewData["Title"] = "Dashboard";

            ConsoleModel console = _consoleRepositorio.ListForID(id);

            List<AgendamentoModel> AgendaConsole = _agendaRepositorio.ListForName(console.Name);
            return View(AgendaConsole);
        }
    }
}
