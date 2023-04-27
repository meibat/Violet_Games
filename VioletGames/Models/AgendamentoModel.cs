using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VioletGames.Data.Enums;

namespace VioletGames.Models
{
    public class AgendamentoModel
    {
        //Colunas da tabela
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Usuário!")]
        public string LoginUser { get; set; }
        [Required(ErrorMessage = "Cliente!")]
        public string CPFClient { get; set; }
        public string NameClient { get; set; }
        [Required(ErrorMessage = "Nome!")]
        public string NameGameOrConsole { get; set; }
        [Required(ErrorMessage = "Categoria!")]
        public CategoryProduct Category { get; set; }
        [Required(ErrorMessage = "Data Agendada!")]
        public DateTime DateSchedule { get; set; }
        public DateTime DateEnter { get; set; } //data de agendamento
        #nullable enable
        public DateTime? DateClose { get; set; } //data devolução ou de saída
        public string? HourtoUse { get; set; } //horas de uso
        #nullable disable
        public double TotalValue { get; set; }
        public StatusPayment Payment { get; set; }
    }
}
