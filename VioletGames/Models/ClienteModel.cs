﻿using System;
using System.ComponentModel.DataAnnotations;
using VioletGames.Data.Enums;

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

        [Required(ErrorMessage = "Telefone!")]
        public string Phone { get; set; }

        #nullable enable
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public int? Number { get; set; }
        public string? CEP { get; set; }
        [EmailAddress(ErrorMessage = "E-mail Inválido!")]
        public string? Email { get; set; }
        #nullable disable

        public Plan Plano { get; set; }
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime PlanDay { get; set; }
        public StatusPayment payment { get; set; }
    }

    public class PlanoModel
    {
        //Colunas da tabela
        public int Id { get; set; }
        public string CPF { get; set; }
        public Plan Plano { get; set; }

        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime PlanDay { get; set; }

#nullable enable
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime? PaymentDate { get; set; }
#nullable disable
        public StatusPayment payment { get; set; }
    }
}
