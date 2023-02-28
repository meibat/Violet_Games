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
        CaixaView ListForIDItem(int id);

        List<CaixaView> SearchAll();

        CaixaView AddItem(CaixaView item);

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

        public CaixaView ListForIDItem(int id)
        {
            throw new NotImplementedException();
        }

        public List<CaixaView> SearchAll()
        {
            throw new NotImplementedException();
            //return _bancoContent.ItemPedidos.ToList();
        }

        public CaixaView AddItem(CaixaView item)
        {
           // _bancoContent.ItemPedidos.Add(item);
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
