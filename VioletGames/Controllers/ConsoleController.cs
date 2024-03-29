﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VioletGames.Models;
using VioletGames.Data.Repositorio;
using VioletGames.Data.Filters;

namespace VioletGames.Controllers
{
    [PageUserLogin]
    public class ConsoleController : Controller
    {
        private readonly IConsoleRepositorio _consoleRepositorio;

        public ConsoleController(IConsoleRepositorio consoleRepositorio)
        {
            _consoleRepositorio = consoleRepositorio;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Consoles";
            List<ConsoleModel> consoles = _consoleRepositorio.SearchAll();

            return View(consoles);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Consoles";
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Consoles";
            ConsoleModel console = _consoleRepositorio.ListForID(id);
            
            return View(console);
        }

        public IActionResult DeleteConfirm(int id)
        {
            ViewData["Title"] = "Consoles";
            ConsoleModel console = _consoleRepositorio.ListForID(id);

            return View(console);
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

