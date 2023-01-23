using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Data.Enums;

namespace VioletGames.Models
{
    public class AgendamentoModel
    {
        //Colunas da tabela
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //ok
        [Required(ErrorMessage = "Usuário!")]
        public string LoginUser { get; set; }
        [Required(ErrorMessage = "Cliente!")]//ok
        public string CPFClient { get; set; }
        [Required(ErrorMessage = "Nome!")]
        public string NameGameOrConsole { get; set; }
        [Required(ErrorMessage = "Categoria!")]
        public CategoryProduct Category { get; set; }
        [Required(ErrorMessage = "Data Agendada!")]
        public DateTime DateSchedule { get; set; }
        #nullable enable
        public DateTime? DateEnter { get; set; }
        public DateTime? DateClose { get; set; }
        #nullable disable
        public float TotalValue { get; set; }
        public StatusPayment Payment { get; set; }
    }
}
