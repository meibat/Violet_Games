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
using VioletGames.Util.Validator;

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

        public IActionResult PayPlan(ClienteModel cliente)
        {

            ItemPedidoModel item = _caixaRepositorio.SearchPlan(cliente);

            if(item != null) JsonUtil.jsonItemSerialize(item);

            return RedirectToAction("Index");
        }

        public IActionResult PayScheduling(AgendamentoModel agendamento)
        {
            ItemPedidoModel item = new ItemPedidoModel();

            item.CategoryProduct = agendamento.Category;
            item.ClientCPF = agendamento.CPFClient;
            item.NameProduct = $"Locação {agendamento.Id} - {agendamento.NameGameOrConsole} - {agendamento.HourtoUse}";
            item.QtdOrder = 1;
            item.QtdAvailable = 1;
            item.PriceUnity = (float)agendamento.TotalValue;
            item.PriceTotal = (float)agendamento.TotalValue;

            JsonUtil.jsonItemSerialize(item);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Pesquisar(ItemPedidoModel item)
        {
            if (item.NameProduct != null) _caixaRepositorio.SearchProduct(item);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddItemPedido(ItemPedidoModel Item) {
            if(Item.ClientCPF != null)
            {
                if (Validator.IsCPF(Item.ClientCPF))
                {
                    _caixaRepositorio.AddItem(Item); 
                    return RedirectToAction("Index");
                }

                TempData["MessagemError"] = $"CPF Inválido!";
                return RedirectToAction("Index");
            }
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
        public IActionResult RemoverProduto(ItemPedidoModel item)
        {
            _caixaRepositorio.RemoveItem(item);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Cancelar()
        {
            _caixaRepositorio.LimparVenda();

            return RedirectToAction("Index");
        }

    }
}
