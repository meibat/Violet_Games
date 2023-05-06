using System;
using System.Collections.Generic;
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
        public ItemPedidoModel SearchPlan(ClienteModel cliente);
    }

    public class CaixaRepositorio : ICaixaRepositorio
    {

        //Extrai variavel bancoContext
        private readonly BancoContent _bancoContent;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IAgendamentoRepositorio _agendamentoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;

        public CaixaRepositorio(BancoContent bancoContent, IProdutoRepositorio produtoRepositorio,
                                IAgendamentoRepositorio agendamentoRepositorio, IClienteRepositorio clienteRepositorio)
        {
            //injecao de dependencia
            _bancoContent = bancoContent;
            _produtoRepositorio = produtoRepositorio;
            _agendamentoRepositorio = agendamentoRepositorio;
            _clienteRepositorio = clienteRepositorio;
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
                valores.ValueSubTotal += item.PriceTotal;
                valores.ValueSubTotal = Math.Round(valores.ValueSubTotal, 2, MidpointRounding.ToZero);

                valores.ValueTotal = valores.ValueSubTotal;
                JsonUtil.jsonCaixaSerialize(valores);
            }
            else
            {
                CaixaModel valoresnovos = new CaixaModel();
                valoresnovos.ValueSubTotal = item.PriceTotal;
                valoresnovos.ValueSubTotal = Math.Round(valoresnovos.ValueSubTotal, 2, MidpointRounding.AwayFromZero);

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
            if (caixa.ValueReceived > 0)
            {
                valores.ValueChange = caixa.ValueReceived - caixa.ValueTotal;
                valores.ValueChange = Math.Round(valores.ValueChange, 2, MidpointRounding.ToZero);
            }

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
                        if (produto != null)
                        {
                            produto.QtdAvailable -= item.QtdOrder;
                            item.DateOrder = DateTime.Now;

                            _bancoContent.ItemPedido.Add(item); //Add os itens do pedido
                            _bancoContent.Produtos.Update(produto);//Atualiza a quantidade do produto
                            _bancoContent.SaveChanges();
                        }
                        else if(item.NameProduct.Substring(0,7) == "Locação")
                        {
                            string[] palavras = item.NameProduct.Split(" ");

                            int idAgendamento = Convert.ToInt32(palavras[1]);
                            AgendamentoModel agendamento = _agendamentoRepositorio.ListForID(idAgendamento);

                            agendamento.Payment = StatusPayment.Pago;

                            _bancoContent.ItemPedido.Add(item); //Add os itens do pedido
                            _bancoContent.Agendamentos.Update(agendamento);//Atualiza o status do agendamento
                            _bancoContent.SaveChanges();
                        }
                        else if (item.NameProduct.Substring(0, 5) == "Plano")
                        {
                            //Update Plano para pago
                            string[] palavras = item.NameProduct.Split(" ");

                            int idPlano = Convert.ToInt32(palavras[1]);

                            PlanoModel plano = _clienteRepositorio.ListPlanForID(idPlano);

                            plano.payment = StatusPayment.Pago;
                            plano.PaymentDate = DateTime.Now;

                            //Add plano do próximo mês
                            ClienteModel cliente = _clienteRepositorio.ListForCPF(ClientCPF);
                            PlanoModel ProximoPlano = new PlanoModel();

                            ProximoPlano.CPF = cliente.CPF;
                            ProximoPlano.Plano = cliente.Plano; ;
                            ProximoPlano.payment = Enums.StatusPayment.Pendente;
                            cliente.PlanDay = ProximoPlano.PlanDay = cliente.PlanDay.AddMonths(1);

                            _bancoContent.ItemPedido.Add(item); //Add os itens do pedido
                            _bancoContent.Planos.Add(ProximoPlano); //Add plano do próximo mês
                            _bancoContent.Planos.Update(plano);//Atualiza o status do agendamento
                            _bancoContent.Clientes.Update(cliente);//Atualiza plano do próximo mês
                            _bancoContent.SaveChanges();
                        }
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
                    if(item.NameProduct != itemList.NameProduct){
                        itensPedido.Add(itemList);
                    }
                    else RemoveValue(itemList);
                }
            JsonUtil.jsonItensSerialize(itensPedido);
        }

        private static void RemoveValue(ItemPedidoModel item)
        {
            CaixaModel valores = JsonUtil.jsonCaixaDeserialize();

            if (valores != null)
            {
                //Sub-Total e Total
                valores.ValueSubTotal -= item.PriceTotal;
                valores.ValueSubTotal = Math.Round(valores.ValueSubTotal, 2, MidpointRounding.ToZero);

                valores.ValueTotal = valores.ValueSubTotal;
                JsonUtil.jsonCaixaSerialize(valores);
            }  
        }

        public void LimparVenda()
        {
            Clean.jsonCaixaClean();
            Clean.jsonItemClean();
            Clean.jsonItensClean();
        }

        public ItemPedidoModel SearchPlan(ClienteModel cliente)
        {
            ItemPedidoModel item = new ItemPedidoModel();
            PlanoModel plano = _clienteRepositorio.ListPlanForClient(cliente); 

            if (plano != null)
            {
                item.CategoryProduct = CategoryProduct.Diversos;
                item.ClientCPF = cliente.CPF;
                item.NameProduct = $"Plano {plano.Id} - {cliente.Plano.ToString()}";
                item.QtdOrder = 1;
                item.QtdAvailable = 1;
                item.PriceUnity = (float)cliente.Plano;
                item.PriceTotal = (float)cliente.Plano;
            }
            return item;
        }
    }
}
