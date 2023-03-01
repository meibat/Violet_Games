using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;

namespace VioletGames.Data.Repositorio
{
    public interface ICaixaRepositorio
    {
        //metodos
        ItemPedidoModel ListForIDItem(int id);

        List<ItemPedidoModel> SearchAll(/*ItemPedidoModel item*/);

        ItemPedidoModel AddItem(ItemPedidoModel item);

        PedidoModel AddPedido(PedidoModel pedido);

        bool Delete(int id);
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

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ItemPedidoModel ListForIDItem(int id)
        {
            throw new NotImplementedException();
        }

        public List<ItemPedidoModel> SearchAll(/*ItemPedidoModel item*/)
        {
            var itens = _bancoContent.ItemPedidos/*.Where(x => x.ClientCPF == item.ClientCPF).Where(x => x.DateOrder == item.DateOrder)*/.ToList();

            return itens;
        }

        public ItemPedidoModel AddItem(ItemPedidoModel item)
        {
            item.DateOrder = DateTime.Now;

            _bancoContent.ItemPedidos.Add(item);
            _bancoContent.SaveChanges();

            return item;
        }

        public PedidoModel AddPedido(PedidoModel pedido)
        {
            _bancoContent.Pedidos.Add(pedido);
            _bancoContent.SaveChanges();

            return pedido;
        }
    }
}
