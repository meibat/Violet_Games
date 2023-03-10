using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VioletGames.Models;

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
            List<ItemPedidoModel> itens = new List<ItemPedidoModel>();

            return View(itens);
        }
    }

    public class ProdutoDescricao : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //string jsonString = File.ReadAllText("../VioletGames/Data/ItemPedido.json");
            //ItemPedidoModel Item = System.Text.Json.JsonSerializer.Deserialize<ItemPedidoModel>(jsonString)!;

            return View();
        }
    }

    public class ValoresCompra : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string jsonString = File.ReadAllText("../VioletGames/Data/Caixa.json");
            CaixaModel valores = System.Text.Json.JsonSerializer.Deserialize<CaixaModel>(jsonString)!;

            return View(valores);
        }
    }
}
