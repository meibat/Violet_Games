using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        public IActionResult Pesquisar(ItemPedidoModel item)
        {
            if (item.NameProduct != null) _caixaRepositorio.SearchProduct(item);
            item.QtdOrder = 0;
            item.PriceTotal = 0;
            //string jsonItem = JsonConvert.SerializeObject(item);
            
            //Criar metodo de serialização, deserializacao dos json para cada e usar aqui
            //File.WriteAllText("../VioletGames/Data/ItemPedido.json", jsonItem);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddItemPedido(ItemPedidoModel Item) {
            
            _caixaRepositorio.AddItem(Item);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult TotalVenda(CaixaModel caixa)
        {
            if(caixa.ValueReceived > 0 || caixa.Desconto > 0) _caixaRepositorio.AddVenda(caixa);

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
