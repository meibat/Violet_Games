using System;
using System.ComponentModel.DataAnnotations;
using VioletGames.Enums;

namespace VioletGames.Models
{
    public class UsuarioModel
    {
        //Colunas da tabela
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Login!")]
        public string Login { get; set; }
        [Required(ErrorMessage = "E-mail!")]
        [EmailAddress(ErrorMessage="E-mail Inválido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Perfil!")]
        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "Senha!")]
        public string Passwd { get; set; }
        public DateTime DateSingIn { get; set; }
        
        #nullable enable
        public DateTime? DateRefresh { get; set; } //pode ser nulo
        #nullable disable

        public bool PasswdValid(String passwd)
        {
            return Passwd == passwd;
        }
    }
}
