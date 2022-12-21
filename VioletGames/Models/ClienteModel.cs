using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VioletGames.Models
{
    public class ClienteModel
    {
        //Colunas da tabela
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Data de Nascimento!")]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime DateBirthday { get; set; }

        [Required(ErrorMessage = "CPF!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Contato!")]
        public ContatoModel Contato { get; set; }
    }
}
