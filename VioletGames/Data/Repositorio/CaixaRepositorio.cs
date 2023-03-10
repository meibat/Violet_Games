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
        //Criar metodo de remocao
        public ItemPedidoModel SearchProduct(ItemPedidoModel item);
        public ItemPedidoModel AddItem(ItemPedidoModel item);
        public void AddVenda(CaixaModel caixa); //Privado
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

        //Void
        public ItemPedidoModel SearchProduct(ItemPedidoModel item)
        {
            //Criar pesquisa  do produto com msg de erro caso encontre
            //Caso encontre verificar se há disponivel no estoque.
            //jogar as ifos do produto no arquivo itemPedido.json
            return item;
        }

        //Void
        public ItemPedidoModel AddItem(ItemPedidoModel item)
        {
            //usar o metodo de procura do item para verificar o estoque

            string jsonItensPedido = File.ReadAllText("../VioletGames/Data/ItensPedido.json");
            Console.WriteLine(jsonItensPedido);
            List<ItemPedidoModel> itensPedido = (List<ItemPedidoModel>)JsonConvert.DeserializeObject(jsonItensPedido);

            Console.WriteLine(itensPedido);

            itensPedido.Add(item);

            //Criar lista pra puxar lista e add novo item
            string jsonString = JsonConvert.SerializeObject(itensPedido);
            File.WriteAllText("../VioletGames/Data/ItensPedido.json", jsonString);

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
                //Salvar compra primeiro depois add os itens no banco, depois diminuir numeros de estoque

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
