using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VioletGames.Models
{
    public class ProdutoModel
    {
        //Colunas da tabela
        public int Id { get; set; }
        [Required(ErrorMessage="Nome!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Quantidade!")]
        public int QtdAvailable { get; set; }
        [Required(ErrorMessage = "Preço!")]
        public float PriceUnity { get; set; }
        
        #nullable enable
        public string? Category { get; set; }
        #nullable disable
    }
}
