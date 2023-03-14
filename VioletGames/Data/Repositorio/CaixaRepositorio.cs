using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;
using VioletGames.Util.JsonUtil;

namespace VioletGames.Data.Repositorio
{
    public interface ICaixaRepositorio
    {
        public ItemPedidoModel SearchProduct(ItemPedidoModel item);
        public void AddItem(ItemPedidoModel item);
        public void AddVenda(CaixaModel caixa); 
        public Boolean GerarVenda(string LoginUser, string ClientCPF);
    }

    public class CaixaRepositorio : ICaixaRepositorio
    {

        //Extrai variavel bancoContext
        private readonly BancoContent _bancoContent;
        private readonly IProdutoRepositorio _produtoRepositorio;

        public CaixaRepositorio(BancoContent bancoContent, IProdutoRepositorio produtoRepositorio)
        {
            //injecao de dependencia
            _bancoContent = bancoContent;
            _produtoRepositorio = produtoRepositorio;
        }

        public ItemPedidoModel SearchProduct(ItemPedidoModel item)
        {
            ProdutoModel produto = _produtoRepositorio.ListForName(item.NameProduct);

            if(produto != null)
            {
                item.PriceUnity = produto.PriceUnity;
                item.QtdAvailable = produto.QtdAvailable;
                item.CategoryProduct = produto.CategoryProduct;

                //string jsonItem = JsonConvert.SerializeObject(item);
                //File.WriteAllText("../VioletGames/Data/ItemPedido.json", jsonItem);

                JsonUtil.jsonCaixaSerialize(item);               
            }
            return item;
        }

        public void AddItem(ItemPedidoModel item)
        {
            //usar o metodo de procura do item para verificar o estoque
            SearchProduct(item);

            if (item.QtdAvailable > 0 && item.QtdOrder > 0)
            {
                //string jsonItensPedido = File.ReadAllText("../VioletGames/Data/ItensPedido.json");
                //List<Object> itensPedido = JsonConvert.DeserializeObject<List<Object>>(jsonItensPedido);

                List<Object> itensPedido = JsonUtil.jsonItensDeserialize();

                item.PriceTotal = item.PriceUnity * item.QtdOrder;
                //string jsonItem = JsonConvert.SerializeObject(item);
                //File.WriteAllText("../VioletGames/Data/ItemPedido.json", jsonItem);

                JsonUtil.jsonItemSerialize(item);

                if (itensPedido != null)
                {
                    itensPedido.Add(item);
                    //string jsonString = JsonConvert.SerializeObject(itensPedido);
                    //File.WriteAllText("../VioletGames/Data/ItensPedido.json", jsonString);

                    JsonUtil.jsonItensSerialize(itensPedido);
                }
                else
                {
                    List<ItemPedidoModel> itens = new List<ItemPedidoModel>();
                    itens.Add(item);
                    //string jsonString = JsonConvert.SerializeObject(itens);
                    //File.WriteAllText("../VioletGames/Data/ItensPedido.json", jsonString);

                    JsonUtil.jsonItensSerialize(itens);
                }

                AddValue(item);
            }
        }

        private static void AddValue(ItemPedidoModel item)
        {
            //string jsonValores = File.ReadAllText("../VioletGames/Data/Caixa.json");
            //CaixaModel valores = JsonConvert.DeserializeObject<CaixaModel>(jsonValores)!;

            CaixaModel valores = JsonUtil.jsonCaixaDeserialize();

            //Sub-Total e Total
            valores.ValueSubTotal = item.PriceTotal + valores.ValueSubTotal;
            valores.ValueTotal = valores.ValueSubTotal;

            //string jsonString = JsonConvert.SerializeObject(valores);
            //File.WriteAllText("../VioletGames/Data/Caixa.json", jsonString);
            JsonUtil.jsonCaixaSerialize(valores);
        }

        public void AddVenda(CaixaModel caixa)
        {
            //string jsonValores = File.ReadAllText("../VioletGames/Data/Caixa.json");
            //CaixaModel valores = JsonConvert.DeserializeObject<CaixaModel>(jsonValores)!;

            CaixaModel valores = JsonUtil.jsonCaixaDeserialize();
            
            //Desconto
            double desconto = (caixa.Desconto / 100) * caixa.ValueSubTotal;
            valores.Desconto = caixa.Desconto;

            //Total Compra
            valores.ValueTotal = caixa.ValueSubTotal - desconto;

            //valor recebido
            valores.ValueReceived = caixa.ValueReceived;

            //Troco
            if(caixa.ValueReceived > 0) valores.ValueChange = caixa.ValueReceived - caixa.ValueTotal;

            //string jsonString = JsonConvert.SerializeObject(valores);
            //File.WriteAllText("../VioletGames/Data/Caixa.json", jsonString);
            
            JsonUtil.jsonCaixaSerialize(valores);
        }

        public bool GerarVenda(string LoginUser, string ClientCPF)
        {
            try
            {
                //Salvar compra primeiro depois add os itens no banco, depois diminuir numeros de estoque

                //string jsonValores = File.ReadAllText("../VioletGames/Data/Caixa.json");
                //CaixaModel caixa = JsonConvert.DeserializeObject<CaixaModel>(jsonValores)!;

                //string jsonItemPedido = File.ReadAllText("../VioletGames/Data/ItemPedido.json");
                //ItemPedidoModel itemPedido = JsonConvert.DeserializeObject<ItemPedidoModel>(jsonItemPedido)!;
                
                CaixaModel caixa = JsonUtil.jsonCaixaDeserialize();
                ItemPedidoModel itemPedido = JsonUtil.jsonItemDeserialize();

                PedidoModel pedido = new PedidoModel();
                pedido.LoginUser = LoginUser;
                pedido.ClientCPF = ClientCPF;
                pedido.Pedido = itemPedido;
                pedido.ValueTotal = caixa.ValueTotal;
                pedido.DateSale = DateTime.Now;

                _bancoContent.Pedidos.Add(pedido);
                _bancoContent.SaveChanges();

                ClearSale(); //Limpar json

                return true;
            }
            catch { return false; }
        }

        private static void ClearSale()
        {
            //Limpando itens
            string jsonItens = JsonConvert.SerializeObject(new List<ItemPedidoModel>());
            File.WriteAllText("../VioletGames/Data/ItensPedido.json", jsonItens);

            //Limpando item
            string jsonItem = JsonConvert.SerializeObject(new ItemPedidoModel());
            File.WriteAllText("../VioletGames/Data/ItemPedido.json", jsonItem);

            //Limpando caixa
            string jsonValores = JsonConvert.SerializeObject(new CaixaModel());
            File.WriteAllText("../VioletGames/Data/Caixa.json", jsonValores);
        }
    }
}
