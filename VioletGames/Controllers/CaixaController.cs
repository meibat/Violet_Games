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
        private readonly ICaixaRepositorio _caixaRepositorio;

        public CaixaController(ICaixaRepositorio caixaRepositorio)
        {
            _caixaRepositorio = caixaRepositorio;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Caixa";

            return View();
        }

        [HttpPost]
        public IActionResult AddItemPedido(ItemPedidoModel Item) {
            
            _caixaRepositorio.AddItem(Item);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult TotalVenda(CaixaModel caixa)
        {
            if(caixa.ValueReceived > 0) _caixaRepositorio.AddVenda(caixa);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult GerarVenda()
        {
            

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Cancelar()
        {
            return RedirectToAction("Index");
        }
    }
}
