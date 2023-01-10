using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using VioletGames.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace VioletGames.Models
{
    //Colunas da tabela
    public class FuncionarioModel

    {
        [Key()]
        public int Id { get; set; }

        [Required(ErrorMessage="Nome!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-mail!")]
        [EmailAddress(ErrorMessage="E-mail Inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Data de nascimento!")]
        [DisplayFormat(DataFormatString="dd/mm/yyyy")]
        public DateTime DateBirthday { get; set; }

        [Required(ErrorMessage = "CPF!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Cargo!")]
        public Office Office { get; set; }

        [Required(ErrorMessage = "Telefone!")]
        public string Phone { get; set; }

        #nullable enable
        public string? RG { get; set; }

        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime? DateAdmission { get; set; }

        public float? Pay { get; set; }

        public string? State { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public int? Number { get; set; }
        public string? CEP { get; set; }
        #nullable disable
    }
}
