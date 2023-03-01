using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Data.Filters;
using VioletGames.Data.Helper;
using VioletGames.Data.Repositorio;
using VioletGames.Models;

namespace VioletGames.Controllers
{
    [PageUserLogin]
    public class CaixaController : Controller
    {
        private readonly IAgendamentoRepositorio _agendamentoRepositorio;
        private readonly IJogoRepositorio _jogoRepositorio;
        private readonly IConsoleRepositorio _consoleRepositorio;
        private readonly ISessionUser _session;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ICaixaRepositorio _caixaRepositorio;

        public CaixaController(IAgendamentoRepositorio agendamentoRepositorio, IJogoRepositorio jogoRepositorio,
                                        IConsoleRepositorio consoleRepositorio, ISessionUser sessionUser,
                                        IClienteRepositorio clienteRepositorio, ICaixaRepositorio caixaRepositorio)
        {
            _agendamentoRepositorio = agendamentoRepositorio;
            _caixaRepositorio = caixaRepositorio;
            _jogoRepositorio = jogoRepositorio;
            _consoleRepositorio = consoleRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _session = sessionUser;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Caixa";
            List<ItemPedidoModel> itens = _caixaRepositorio.SearchAll();
            
            return View(itens);
        }

        [HttpPost]
        public IActionResult AddItem(ItemPedidoModel item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _caixaRepositorio.AddItem(item);
                    TempData["MessagemSucess"] = "Console cadastrado com sucesso!";
                    return RedirectToAction("Index", item);
                }
                return View("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível efetuar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
