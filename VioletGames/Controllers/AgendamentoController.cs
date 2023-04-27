using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VioletGames.Models;
using VioletGames.Data.Repositorio;
using VioletGames.Data.Filters;
using VioletGames.Data.Enums;
using VioletGames.Data.Helper;
using VioletGames.Util.Validator;

namespace VioletGames.Controllers
{
    [PageUserLogin]
    public class AgendamentoController : Controller
    {
        private readonly IAgendamentoRepositorio _agendamentoRepositorio;
        private readonly IJogoRepositorio _jogoRepositorio;
        private readonly IConsoleRepositorio _consoleRepositorio;
        private readonly ISessionUser _session;
        private readonly IClienteRepositorio _clienteRepositorio;

        public AgendamentoController(IAgendamentoRepositorio agendamentoRepositorio, IJogoRepositorio jogoRepositorio, 
                                        IConsoleRepositorio consoleRepositorio ,ISessionUser sessionUser,
                                        IClienteRepositorio clienteRepositorio)
        {
            _agendamentoRepositorio = agendamentoRepositorio;
            _jogoRepositorio = jogoRepositorio;
            _consoleRepositorio = consoleRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _session = sessionUser;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Agendamentos";
            List<AgendamentoModel> agendamentos = _agendamentoRepositorio.SearchAll();

            return View(agendamentos);
        }

        public IActionResult CreateConsole(int id)
        {
            ViewData["Title"] = "Agendamentos";
            AgendamentoModel agendamento = new AgendamentoModel();
            try
            {
                UsuarioModel usuarioLogin = _session.SeachSessionUser();
                ConsoleModel console = _consoleRepositorio.ListForID(id);

                agendamento.LoginUser = usuarioLogin.Login;
                agendamento.DateSchedule = DateTime.Today;
                agendamento.DateEnter = DateTime.Now;
                agendamento.Payment = StatusPayment.Pendente;

                if (console != null)
                {
                    agendamento.NameGameOrConsole = console.Name;
                    agendamento.Category = CategoryProduct.Console;
                    agendamento.TotalValue = console.PriceHour;
                }
                return View(agendamento);
            }
            catch (System.Exception erro) {
                TempData["MessagemError"] = $"Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult CreateGame(int id)
        {
            ViewData["Title"] = "Agendamentos";
            AgendamentoModel agendamento = new AgendamentoModel();
            try
            {
                UsuarioModel usuarioLogin = _session.SeachSessionUser();
                JogoModel jogo = _jogoRepositorio.ListForID(id);

                agendamento.LoginUser = usuarioLogin.Login;
                agendamento.DateSchedule = DateTime.Today;
                agendamento.DateEnter = DateTime.Now;
                agendamento.Payment = StatusPayment.Pendente;

                if (jogo != null)
                {
                    agendamento.Category = CategoryProduct.Game;
                    agendamento.NameGameOrConsole = jogo.Name;
                    agendamento.TotalValue = jogo.PriceHour;
                }

                return View(agendamento);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Agendamentos";
            AgendamentoModel agendamento = _agendamentoRepositorio.ListForID(id);
            
            return View(agendamento);
        }

        public IActionResult DeleteConfirm(int id)
        {
            ViewData["Title"] = "Agendamentos";
            AgendamentoModel agendamento = _agendamentoRepositorio.ListForID(id);

            return View(agendamento);
        }

        public IActionResult Delete(int id)
        {
            try{
                bool deleted = _agendamentoRepositorio.Delete(id);
                
                if (deleted)
                {
                    TempData["MessagemSucess"] = "Agendamento deletado com sucesso!";
                }
                else
                {
                    TempData["MessagemError"] = $"Não foi possível deletar o cadastro! Tente novamente.";
                }
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível deletar o cadastro! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult PayScheduling(int id)
        {
            AgendamentoModel agendamento = _agendamentoRepositorio.ListForID(id);

            return RedirectToAction("PayScheduling", "Caixa", agendamento);
        }

        //Métodos Post
        [HttpPost]
        public IActionResult CreateGame(AgendamentoModel agendamento)
        {
            try{
                if (ModelState.IsValid) 
                {
                    if (!Validator.IsCPF(agendamento.CPFClient))
                    {
                        TempData["MessagemError"] = "CPF inválido!";
                        return View(agendamento);
                    }

                    if (!_clienteRepositorio.isClient(agendamento.CPFClient))
                    {
                        TempData["MessagemError"] = "Cliente não encontrado!";
                        return View(agendamento);
                    }

                    ClienteModel cliente = _clienteRepositorio.ListForCPF(agendamento.CPFClient);
                    agendamento.NameClient = cliente.Name;

                    _agendamentoRepositorio.Create(agendamento);
                    TempData["MessagemSucess"] = "Agendamento cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(agendamento);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível efetuar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult CreateConsole(AgendamentoModel agendamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(agendamento.DateClose.Value.Day != agendamento.DateEnter.Day ||
                    agendamento.DateClose.Value.Month != agendamento.DateEnter.Month)
                    {
                        TempData["MessagemError"] = "Data de agendamento de console inválida!";
                        return View(agendamento);
                    }
                    if (!Validator.IsCPF(agendamento.CPFClient))
                    {
                        TempData["MessagemError"] = "CPF inválido!";
                        return View(agendamento);
                    }

                    if (!_clienteRepositorio.isClient(agendamento.CPFClient))
                    {
                        //Criar msgbox: deseja cadastrar o cliente novo?
                        TempData["MessagemError"] = "Cliente não encontrado!";
                        return View(agendamento);
                    }
                    ClienteModel cliente = _clienteRepositorio.ListForCPF(agendamento.CPFClient);
                    agendamento.NameClient = cliente.Name;

                    _agendamentoRepositorio.Create(agendamento);
                    TempData["MessagemSucess"] = "Agendamento cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(agendamento);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível efetuar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(AgendamentoModel agendamento)
        {
            try{
                if (ModelState.IsValid)
                {
                    if (!Validator.IsCPF(agendamento.CPFClient))
                    {
                        TempData["MessagemError"] = "CPF inválido!";
                        return View(agendamento);
                    }

                    if (!_clienteRepositorio.isClient(agendamento.CPFClient))
                    {
                        TempData["MessagemError"] = "Cliente não encontrado!";
                        return View(agendamento);
                    }
                    ClienteModel cliente = _clienteRepositorio.ListForCPF(agendamento.CPFClient);
                    agendamento.NameClient = cliente.Name;
                    
                    if(agendamento.Category == CategoryProduct.Console)
                    {
                        ConsoleModel console = _consoleRepositorio.ListForName(agendamento.NameGameOrConsole);
                        agendamento.TotalValue = console.PriceHour;
                    }
                    if (agendamento.Category == CategoryProduct.Game)
                    {
                        JogoModel jogo = _jogoRepositorio.ListForName(agendamento.NameGameOrConsole);
                        agendamento.TotalValue = jogo.PriceHour;
                    }

                    _agendamentoRepositorio.Update(agendamento);
                    TempData["MessagemSucess"] = "Cadastro editado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(agendamento);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível editar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult SeachForCPF(string CPF)
        {
            ClienteModel cliente = _clienteRepositorio.ListForCPF(CPF);
            if (cliente != null) { return Json(cliente.Name); }
            return Json(null);
        }
    }
}

