using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;
using VioletGames.Data.Repositorio;

namespace VioletGames.Controllers
{
    public class ConsoleController : Controller
    {
        private readonly IConsoleRepositorio _consoleRepositorio;

        public ConsoleController(IConsoleRepositorio consoleRepositorio)
        {
            _consoleRepositorio = consoleRepositorio;
        }

        public IActionResult Index()
        {
            List<ConsoleModel> consoles = _consoleRepositorio.SearchAll();

            return View(consoles);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            ConsoleModel console = _consoleRepositorio.ListForID(id);
            return View();
        }

        public IActionResult DeleteConfirm(int id)
        {
            ConsoleModel console = _consoleRepositorio.ListForID(id);

            return View();
        }

        public IActionResult Delete(int id)
        {
            try{
                bool deleted = _consoleRepositorio.Delete(id);
                
                if (deleted)
                {
                    TempData["MessagemSucess"] = "Console deletado com sucesso!";
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
        public IActionResult Create(ConsoleModel console)
        {
            try{
                if (ModelState.IsValid) 
                {
                    _consoleRepositorio.Create(console);
                    TempData["MessagemSucess"] = "Console cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(console);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível efetuar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(ConsoleModel console)
        {
            try{
                if (ModelState.IsValid)
                {
                    _consoleRepositorio.Update(console);
                    TempData["MessagemSucess"] = "Cadastro editado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(console);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível editar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}

