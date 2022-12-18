using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VioletGames.Models
{
    public class ContatoModel
    {
        //Colunas da tabela
        public int Id { get; set; }
        [Required(ErrorMessage = "Telefone!")]
        public string Phone { get; set; }

        #nullable enable
        public string? Address { get; set; }
        #nullable disable
    }
}
