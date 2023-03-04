﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Data.Enums;

namespace VioletGames.Models
{
    public class ItemPedidoModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Nome!")]
        public string NameProduct { get; set; }
        public ProdutoModel produto { get; set; }
        public string ClientCPF { get; set; }
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DateOrder { get; set; }
        [Required(ErrorMessage = "Quantidade!")]
        public int QtdAvailable { get; set; }
        public float PriceUnity { get; set; }
        public float PriceTotal {get; set;}
        [Required(ErrorMessage = "Categoria!")]
        public CategoryProduct CategoryProduct { get; set; }
    }

    public class CaixaModel
    {
        public double ValueSubTotal { get; set; } //Subtotal
        public double Desconto { get; set; } //Desconto
        public double ValueReceived { get; set; } //valor recebido
        public double ValueChange { get; set; } //troco
        public double ValueTotal { get; set; } //Valor total compra
    }

    public class PedidoModel
    {
        public int id { get; set; }
        public string LoginUser { get; set; }
        public String ClientCPF { get; set; }
        public ItemPedidoModel Pedido { get; set; }
        public float ValueTotal { get; set; } //Valor total compra
        public DateTime DateSale { get; set; }
    }
}
