using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VioletGames.Models
{
    public class ResetPasswdModel
    {
        [Required(ErrorMessage = "Digite o login!")]
        public String Login { get; set; }
        [Required(ErrorMessage = "Digite o E-mail!")]
        [EmailAddress(ErrorMessage = "E-mail Inválido!")]
        public String Email { get; set; }
    }
}
