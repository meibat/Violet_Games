using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using VioletGames.Models;

namespace VioletGames.Util.JsonUtil
{
    public class JsonUtil
    {
        //Criar serialização e deserialização
        public static void jsonCaixaSerialize(CaixaModel valores){
            string jsonValores = JsonConvert.SerializeObject(valores);
            File.WriteAllText("../VioletGames/Data/Caixa.json", jsonValores);
        }

        public static void jsonItemSerialize(ItemPedidoModel item){
            string jsonItem = JsonConvert.SerializeObject(item);
            File.WriteAllText("../VioletGames/Data/ItemPedido.json", jsonItem);
        }

        public static void jsonItensSerialize(List<ItemPedidoModel> itens){
            string jsonItens = JsonConvert.SerializeObject(itens);
            File.WriteAllText("../VioletGames/Data/ItensPedido.json", jsonItens);
        }

        public static CaixaModel jsonCaixaDeserialize(){
            string jsonValores = File.ReadAllText("../VioletGames/Data/Caixa.json");
            CaixaModel valores = JsonConvert.DeserializeObject<CaixaModel>(jsonValores)!;
            return valores;
        }

        public static ItemPedidoModel jsonItemDeserialize(){
            string jsonItemPedido = File.ReadAllText("../VioletGames/Data/ItemPedido.json");
            ItemPedidoModel item = JsonConvert.DeserializeObject<ItemPedidoModel>(jsonItemPedido);
            return item;
        }

        public static List<ItemPedidoModel> jsonItensDeserialize(){
            string jsonItensPedido = File.ReadAllText("../VioletGames/Data/ItensPedido.json");
            List<ItemPedidoModel> itensPedido = JsonConvert.DeserializeObject<List<ItemPedidoModel>>(jsonItensPedido);
            return itensPedido;
        }
    }
}
