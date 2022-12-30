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

    public class ResetPasswdUserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite a senha atual!")]
        public String Passwd { get; set; }
        [Required(ErrorMessage = "Digita a nova senha!")]
        public String NewPasswd { get; set; }
        [Required(ErrorMessage = "Digite confirme a nova senha!")]
        [Compare("NewPasswd", ErrorMessage="Senha não Confere!")]
        public String ConfirmNewPasswd { get; set; }
    }
}
