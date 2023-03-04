using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using VioletGames.Models;

namespace VioletGames.Data.Repositorio
{
    public interface ICaixaRepositorio
    {
        public ItemPedidoModel AddItem(ItemPedidoModel item);
        public void AddVenda(CaixaModel caixa);
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
            string fileName = "../VioletGames/Data/ItemPedido.json";
            string jsonString = JsonSerializer.Serialize(item);
            File.WriteAllText(fileName, jsonString);

            AddValue(item);

            return item;
        }

        private static void AddValue(ItemPedidoModel item)
        {
            CaixaModel caixa = new CaixaModel();
            string jsonValores = File.ReadAllText("../VioletGames/Data/Caixa.json");
            CaixaModel valores = JsonSerializer.Deserialize<CaixaModel>(jsonValores)!;

            //Sub-Total
            caixa.ValueSubTotal = item.PriceTotal + valores.ValueSubTotal;

            string jsonString = JsonSerializer.Serialize(caixa);
            File.WriteAllText("../VioletGames/Data/Caixa.json", jsonString);
        }

        public void AddVenda(CaixaModel caixa)
        {
            string jsonValores = File.ReadAllText("../VioletGames/Data/Caixa.json");
            CaixaModel valores = JsonSerializer.Deserialize<CaixaModel>(jsonValores)!;

            //Desconto
            double desconto = (caixa.Desconto / 100) * caixa.ValueSubTotal;

            //Total Compra
            caixa.ValueTotal = caixa.ValueSubTotal - desconto;

            //Troco
            caixa.ValueChange = caixa.ValueReceived - caixa.ValueTotal;

            string jsonString = JsonSerializer.Serialize(caixa);
            File.WriteAllText("../VioletGames/Data/Caixa.json", jsonString);
        }
    }
}
