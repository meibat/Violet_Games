using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;
using VioletGames.Data.Repositorio;
using VioletGames.Data.Filters;
using VioletGames.Data.Enums;

namespace VioletGames.Controllers
{
    [PageUserLogin]
    public class AgendamentoController : Controller
    {
        private readonly IAgendamentoRepositorio _agendamentoRepositorio;
        private readonly IJogoRepositorio _jogoRepositorio;

        public AgendamentoController(IAgendamentoRepositorio agendamentoRepositorio, IJogoRepositorio jogoRepositorio)
        {
            _agendamentoRepositorio = agendamentoRepositorio;
            _jogoRepositorio = jogoRepositorio;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Agendamentos";
            List<AgendamentoModel> agendamentos = _agendamentoRepositorio.SearchAll();

            return View(agendamentos);
        }

        public IActionResult Create(int id)
        {
            ViewData["Title"] = "Agendamentos";
            AgendamentoModel agendamento = new AgendamentoModel();
            try
            {
                JogoModel jogo = _jogoRepositorio.ListForID(id);
                if(jogo != null)
                {
                    agendamento.NameGameOrConsole = jogo.Name;
                    agendamento.LoginUser = "Mei";
                    agendamento.Category = CategoryProduct.Game;
                }
                Console.WriteLine(agendamento.NameGameOrConsole);
                return View(agendamento);
            }
            catch (System.Exception erro) {
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

        //Métodos Post
        [HttpPost]
        public IActionResult Create(AgendamentoModel agendamento)
        {
            try{
                agendamento.Payment = StatusPayment.Pending;

                if (ModelState.IsValid) 
                {
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

    }
}

