using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;

namespace VioletGames.Data.Repositorio
{
    public interface ICaixaRepositorio
    {
        public ItemPedidoModel AddItem(ItemPedidoModel item);
        public void AddVenda(CaixaModel caixa);
        public Boolean GerarVenda(string LoginUser, string ClientCPF);
    }

    public class CaixaRepositorio : ICaixaRepositorio
    {

        //Extrai variavel bancoContext
        private readonly BancoContent _bancoContent;

        public CaixaRepositorio(BancoContent bancoContent)
        {
            //injecao de dependencia
            _bancoContent = bancoContent;
        }

        public ItemPedidoModel AddItem(ItemPedidoModel item)
        {
            string jsonItemPedido = File.ReadAllText("../VioletGames/Data/ItemPedido.json");
            Console.WriteLine(jsonItemPedido);
            List<ItemPedidoModel> itensPedido = (List<ItemPedidoModel>)JsonConvert.DeserializeObject(jsonItemPedido);

            Console.WriteLine(itensPedido);

            itensPedido.Add(item);

            //Criar lista pra puxar lista e add novo item
            string jsonString = JsonConvert.SerializeObject(itensPedido);
            File.WriteAllText("../VioletGames/Data/ItemPedido.json", jsonString);

            AddValue(item);

            return item;
        }

        private static void AddValue(ItemPedidoModel item)
        {
            string jsonValores = File.ReadAllText("../VioletGames/Data/Caixa.json");
            CaixaModel valores = JsonConvert.DeserializeObject<CaixaModel>(jsonValores)!;

            //Sub-Total
            valores.ValueSubTotal = item.PriceTotal + valores.ValueSubTotal;

            string jsonString = JsonConvert.SerializeObject(valores);
            File.WriteAllText("../VioletGames/Data/Caixa.json", jsonString);
        }

        public void AddVenda(CaixaModel caixa)
        {
            string jsonValores = File.ReadAllText("../VioletGames/Data/Caixa.json");
            CaixaModel valores = JsonConvert.DeserializeObject<CaixaModel>(jsonValores)!;

            //Desconto
            double desconto = (caixa.Desconto / 100) * caixa.ValueSubTotal;

            //Total Compra
            valores.ValueTotal = caixa.ValueSubTotal - desconto;

            //Troco
            valores.ValueChange = caixa.ValueReceived - caixa.ValueTotal;

            string jsonString = JsonConvert.SerializeObject(valores);
            File.WriteAllText("../VioletGames/Data/Caixa.json", jsonString);
        }

        public bool GerarVenda(string LoginUser, string ClientCPF)
        {
            try
            {
                string jsonValores = File.ReadAllText("../VioletGames/Data/Caixa.json");
                CaixaModel caixa = JsonConvert.DeserializeObject<CaixaModel>(jsonValores)!;

                string jsonItemPedido = File.ReadAllText("../VioletGames/Data/ItemPedido.json");
                ItemPedidoModel itemPedido = JsonConvert.DeserializeObject<ItemPedidoModel>(jsonItemPedido)!;

                PedidoModel pedido = new PedidoModel();
                pedido.LoginUser = LoginUser;
                pedido.ClientCPF = ClientCPF;
                pedido.Pedido = itemPedido;
                pedido.ValueTotal = caixa.ValueTotal;
                pedido.DateSale = DateTime.Now;

                _bancoContent.Pedidos.Add(pedido);
                _bancoContent.SaveChanges();

                return true;
            }
            catch { return false; }
        }
    }
}
