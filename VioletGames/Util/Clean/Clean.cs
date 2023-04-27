using System.Collections.Generic;
using VioletGames.Models;

namespace VioletGames.Util.Clean
{
    public class Clean
    {
        public static string Date(string dateForClean)
        {
            return dateForClean.Replace("00:00:00", "");
        }

        //Criar limpeza de json
        public static void jsonItemClean(){
            //tentar com um new itemPedido();
            ItemPedidoModel itemPedido = new ItemPedidoModel();

            JsonUtil.JsonUtil.jsonItemSerialize(itemPedido);
        }

        public static void jsonCaixaClean(){
            CaixaModel caixa = new CaixaModel();

            JsonUtil.JsonUtil.jsonCaixaSerialize(caixa);
        }

        public static void jsonItensClean(){
            List<ItemPedidoModel> itensPedido = new List<ItemPedidoModel>();

            JsonUtil.JsonUtil.jsonItensSerialize(itensPedido);
        }
    }
}
