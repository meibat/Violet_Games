using System.ComponentModel.DataAnnotations;
using VioletGames.Data.Enums;

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
        [Required(ErrorMessage = "Categoria!")]
        public CategoryProduct CategoryProduct { get; set; }
    }
}
