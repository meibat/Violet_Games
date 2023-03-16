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
using VioletGames.Util.JsonUtil;

namespace VioletGames.Controllers
{
    [PageUserLogin]
    public class CaixaController : Controller
    {
        private readonly ICaixaRepositorio _caixaRepositorio;
        private readonly ISessionUser _session;

        public CaixaController(ICaixaRepositorio caixaRepositorio, ISessionUser sessionUser)
        {
            _caixaRepositorio = caixaRepositorio;
            _session = sessionUser;
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
            try
            {
                UsuarioModel usuarioLogin = _session.SeachSessionUser();
                ItemPedidoModel item = JsonUtil.jsonItemDeserialize();

                Boolean VendaGerada = _caixaRepositorio.GerarVenda(usuarioLogin.Login, item.ClientCPF);
                
                if (VendaGerada) { 
                    TempData["MessagemSucess"] = "Venda Salva!";
                    return RedirectToAction("Index");
                }

                TempData["MessagemError"] = $"Venda não foi salva";
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Cancelar()
        {
            _caixaRepositorio.LimparVenda();

            return RedirectToAction("Index");
        }
    }
}
