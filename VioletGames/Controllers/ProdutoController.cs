using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Data.Filters;
using VioletGames.Data.Repositorio;
using VioletGames.Models;

namespace VioletGames.Controllers
{
    [PageUserLogin]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoController(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Produtos";
            List<ProdutoModel> produtos = _produtoRepositorio.SearchAll();

            return View(produtos);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Produtos";
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Produtos";
            ProdutoModel produto = _produtoRepositorio.ListForID(id);
            
            return View(produto);
        }

        public IActionResult DeleteConfirm(int id)
        {
            ViewData["Title"] = "Produtos";
            ProdutoModel produto = _produtoRepositorio.ListForID(id);

            return View(produto);
        }

        public IActionResult Delete(int id)
        {
            try{
                bool deleted = _produtoRepositorio.Delete(id);

                if (deleted)
                {
                    TempData["MessagemSucess"] = "Produto deletado com sucesso!";
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
        public IActionResult Create(ProdutoModel produto)
        {
            try{
                if(ModelState.IsValid)
                {
                    _produtoRepositorio.Create(produto);
                    TempData["MessagemSucess"] = "Produto cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(produto);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível efetuar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(ProdutoModel produto)
        {
            try{
                if (ModelState.IsValid)
                {
                    _produtoRepositorio.Update(produto);
                    TempData["MessagemSucess"] = "Cadastro editado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(produto);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível editar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
