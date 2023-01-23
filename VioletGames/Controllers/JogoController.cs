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

namespace VioletGames.Controllers
{
    [PageUserLogin]
    public class JogoController : Controller
    {
        private readonly IJogoRepositorio _jogoRepositorio;

        public JogoController(IJogoRepositorio jogoRepositorio)
        {
            _jogoRepositorio = jogoRepositorio;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Jogos";
            List<JogoModel> jogos = _jogoRepositorio.SearchAll();

            return View(jogos);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Jogos";
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Jogos";
            JogoModel jogo = _jogoRepositorio.ListForID(id);
            
            return View();
        }

        public IActionResult DeleteConfirm(int id)
        {
            ViewData["Title"] = "Jogos";
            JogoModel jogo = _jogoRepositorio.ListForID(id);

            return View();
        }

        public IActionResult Delete(int id)
        {
            try{
                bool deleted = _jogoRepositorio.Delete(id);
                
                if (deleted)
                {
                    TempData["MessagemSucess"] = "Jogo deletado com sucesso!";
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
        public IActionResult Create(JogoModel jogo)
        {
            try{
                if (ModelState.IsValid) 
                {
                    _jogoRepositorio.Create(jogo);
                    TempData["MessagemSucess"] = "Jogo cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(jogo);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível efetuar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(JogoModel jogo)
        {
            try{
                if (ModelState.IsValid)
                {
                    _jogoRepositorio.Update(jogo);
                    TempData["MessagemSucess"] = "Cadastro editado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(jogo);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível editar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}

