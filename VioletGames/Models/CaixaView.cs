using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Data.Enums;

namespace VioletGames.Models
{
    public class CaixaView
    {
        public List<ItemPedidoModel> ListItens { get; set; }
        public ItemPedidoModel Itens { get; set; }
        public PedidoModel Pedido { get; set; }
    }

    public class ItemPedidoModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Nome!")]
        public string NameProduct { get; set; }
        public ProdutoModel produto { get; set; }
        public ClienteModel ClientCPF { get; set; }
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DateOrder { get; set; }
        [Required(ErrorMessage = "Quantidade!")]
        public int QtdAvailable { get; set; }
        public float PriceUnity { get; set; }
        public float PriceTotal {get; set;}
        [Required(ErrorMessage = "Categoria!")]
        public CategoryProduct CategoryProduct { get; set; }
    }

    public class PedidoModel
    {
        public int id { get; set; }
        public string LoginUser { get; set; }
        public ClienteModel ClientCPF { get; set; }
        public ItemPedidoModel Pedido { get; set; }
        public float ValueTotal { get; set; }
        public DateTime DateSale { get; set; }
    }
}
