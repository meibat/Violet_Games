using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;
using VioletGames.Util.JsonUtil;
using VioletGames.Util.Clean;
using VioletGames.Data.Enums;

namespace VioletGames.Data.Repositorio
{
    public interface ICaixaRepositorio
    {
        public ItemPedidoModel SearchProduct(ItemPedidoModel item);
        public void AddItem(ItemPedidoModel item);
        public void RemoveItem(ItemPedidoModel item);
        public void AddVenda(CaixaModel caixa); 
        public Boolean GerarVenda(string LoginUser, string ClientCPF);
        public void LimparVenda();
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

                JsonUtil.jsonItemSerialize(item);               
            }
            return item;
        }

        public void AddItem(ItemPedidoModel item)
        {
            //usar o metodo de procura do item para verificar o estoque
            SearchProduct(item);

            if (item.QtdAvailable > 0 && item.QtdOrder > 0)
            {
                List<ItemPedidoModel> itensPedido = JsonUtil.jsonItensDeserialize();

                item.PriceTotal = item.PriceUnity * item.QtdOrder;
                JsonUtil.jsonItemSerialize(item);

                if (itensPedido != null)
                {
                    itensPedido.Add(item);
                    JsonUtil.jsonItensSerialize(itensPedido);
                }
                else
                {
                    List<ItemPedidoModel> itens = new List<ItemPedidoModel>();
                    
                    itens.Add(item);
                    JsonUtil.jsonItensSerialize(itens);
                }

                AddValue(item);
            }
        }

        private static void AddValue(ItemPedidoModel item)
        {
            CaixaModel valores = JsonUtil.jsonCaixaDeserialize();

            if (valores != null)
            {
                //Sub-Total e Total
                valores.ValueSubTotal = item.PriceTotal + valores.ValueSubTotal;
                valores.ValueTotal = valores.ValueSubTotal;
                JsonUtil.jsonCaixaSerialize(valores);
            }
            else
            {
                CaixaModel valoresnovos = new CaixaModel();
                valoresnovos.ValueSubTotal = (float) item.PriceTotal.ToString($"{item.PriceTotal:F2}");
                valoresnovos.ValueTotal = valoresnovos.ValueSubTotal;
                JsonUtil.jsonCaixaSerialize(valoresnovos);
            }   
        }

        public void AddVenda(CaixaModel caixa)
        {
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

            JsonUtil.jsonCaixaSerialize(valores);
        }

        public bool GerarVenda(string LoginUser, string ClientCPF)
        {
            try
            {
                CaixaModel caixa = JsonUtil.jsonCaixaDeserialize();
                List<ItemPedidoModel> itens = JsonUtil.jsonItensDeserialize();

                if(caixa.ValueReceived > 0)
                {
                    PedidoModel pedido = new PedidoModel();
                    pedido.LoginUser = LoginUser;
                    pedido.ClientCPF = ClientCPF;
                    pedido.ValueTotal = caixa.ValueTotal;
                    pedido.ValueReceived = caixa.ValueReceived;
                    pedido.ValueChange = caixa.ValueChange;
                    pedido.DateSale = DateTime.Now;

                    _bancoContent.Pedidos.Add(pedido); //Add pedidos

                    foreach (ItemPedidoModel item in itens)
                    {
                        ProdutoModel produto = _produtoRepositorio.ListForName(item.NameProduct);
                        produto.QtdAvailable -= item.QtdOrder;
                        item.DateOrder = DateTime.Now;

                        _bancoContent.ItemPedido.Add(item); //Add os itens do pedido
                        _bancoContent.Produtos.Update(produto);//Atualiza a quantidade do produto
                        _bancoContent.SaveChanges();
                    }

                    LimparVenda(); //Limpar json
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public void RemoveItem(ItemPedidoModel item)
        {
            List<ItemPedidoModel> itensList = JsonUtil.jsonItensDeserialize(); //Lista de itens
            List<ItemPedidoModel> itensPedido = new List<ItemPedidoModel>(); //Nova lista

            foreach (ItemPedidoModel itemList in itensList)
                {
                    if(item.NameProduct == itemList.NameProduct){
                        Console.write(item.NameProduct);
                    }
                     itensPedido.Add(itemList);
                }
            JsonUtil.jsonItensSerialize(itensPedido);
        }

        public void LimparVenda()
        {
            Clean.jsonCaixaClean();
            Clean.jsonItemClean();
            Clean.jsonItensClean();
        }
    }
}
