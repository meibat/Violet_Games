using System;
using System.ComponentModel.DataAnnotations;
using VioletGames.Data.Enums;

namespace VioletGames.Models
{
    public class UsuarioWithoutModel
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
        
        #nullable enable
        public DateTime? DateRefresh { get; set; } //pode ser nulo
        #nullable disable
    }
}
