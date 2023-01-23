using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Data.Enums;

namespace VioletGames.Models
{
    public class JogoModel
    {
        //Colunas da tabela
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Preço!")]
        public float PriceHour { get; set; }
        [Required(ErrorMessage = "Categoria!")]
        public CategoryConsole CategoryConsole { get; set; }
        public StatusLocation StatusJogo { get; set; }
    }
}
