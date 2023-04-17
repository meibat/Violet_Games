using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VioletGames.Data.Filters;
using VioletGames.Data.Repositorio;
using VioletGames.Models;
using VioletGames.Data.Enums;

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

        public IActionResult LoadStatusConsole()
        {
            try
            {
                List<AgendamentoModel> AgendaConsole = _agendaRepositorio.SearchAll();

                foreach (AgendamentoModel agenda in AgendaConsole)
                {
                    DateTime dataAtual = DateTime.Now;
                    DateTime dataproximo = DateTime.Now.AddMinutes(30);

                    if (agenda.Category == CategoryProduct.Console)
                    {
                        int compareDateEnter = DateTime.Compare(agenda.DateEnter, dataAtual);
                        int compareDateClose = DateTime.Compare((DateTime)agenda.DateClose, dataAtual);
                        int compareDataProxima = DateTime.Compare(agenda.DateEnter, dataproximo);

                        if (compareDateEnter == -1 && compareDateClose == 1)//sendo usado
                        {
                            ConsoleModel console = _consoleRepositorio.ListForName(agenda.NameGameOrConsole);

                            console.StatusConsole = StatusLocation.Usando;
                            _consoleRepositorio.Update(console);
                            Console.WriteLine($"sendo usado {console.Id}-{console.Name} Data entrada {agenda.DateEnter} dataAtual {dataAtual} Data agendada final {agenda.DateClose}");
                        }
                        if (compareDateEnter == -1 && compareDateClose == -1)//console livre se não desativado
                        {
                            ConsoleModel console = _consoleRepositorio.ListForName(agenda.NameGameOrConsole);

                            if (console.StatusConsole != StatusLocation.Desativado)
                            {
                                console.StatusConsole = StatusLocation.Livre;
                                _consoleRepositorio.Update(console);
                                Console.WriteLine($"console livre {console.Id}-{console.Name} Data entrada {agenda.DateEnter} dataAtual {dataAtual} Data agendada final {agenda.DateClose}");
                            }
                            else
                            {
                                Console.WriteLine($"console desativado");

                            }
                        }
                        if (compareDateEnter == 1 && compareDataProxima == -1)//pendente
                        {
                            ConsoleModel console = _consoleRepositorio.ListForName(agenda.NameGameOrConsole);

                            console.StatusConsole = StatusLocation.Pedente;
                            _consoleRepositorio.Update(console);
                            Console.WriteLine($"pendente {console.Id}-{console.Name} Data entrada {agenda.DateEnter} dataAtual {dataAtual} Data proximo {dataproximo}");
                        }
                    }
                }
                TempData["MessagemSucess"] = $"Status atualizados!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível efetuar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
