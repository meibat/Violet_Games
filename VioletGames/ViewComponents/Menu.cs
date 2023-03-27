using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VioletGames.Models;
using VioletGames.Util.JsonUtil;

namespace VioletGames.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessionUser = HttpContext.Session.GetString("SessionSinginUser");

            if (string.IsNullOrEmpty(sessionUser)) return null;

            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessionUser);

            return View(usuario);
        }
    }

    public class ListaItens : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ItemPedidoModel> itensPedido = JsonUtil.jsonItensDeserialize();

            return View(itensPedido);
        }
    }

    public class ProdutoDescricao : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ItemPedidoModel Item = JsonUtil.jsonItemDeserialize();

            return View(Item);
        }
    }

    public class ValoresCompra : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            CaixaModel valores = JsonUtil.jsonCaixaDeserialize();

            return View(valores);
        }
    }

    public class DashboardAdmin : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }

    public class DashboardStand : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
