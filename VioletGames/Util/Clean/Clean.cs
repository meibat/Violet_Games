using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            ItemPedidoModel itemPedido = JsonUtil.jsonItemDeserialize();

            JsonUtil.jsonItemSerialize(itemPedido);
        }

        public static void jsonCaixaClean(){
            CaixaModel caixa = JsonUtil.jsonCaixaDeserialize();

            JsonUtil.jsonCaixaSerialize(caixa);
        }

        public static void jsonItensClean(){
            List<Object> itensPedido = JsonUtil.jsonItensDeserialize();

            JsonUtil.jsonItensSerialize(itensPedido);
        }
    }
}
