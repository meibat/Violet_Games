using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;

namespace VioletGames.Data.Repositorio 
{
    public interface IUsuarioRepositorio
    {
        //metodos
        UsuarioModel SeachForLogin(string login);
        UsuarioModel SeachForLoginAndEmail(string login, string email);

        UsuarioModel ListForID(int id);

        List<UsuarioModel> SearchAll();

        UsuarioModel Create(UsuarioModel usuario);

        UsuarioModel Update(UsuarioModel usuario);

        UsuarioModel UpdatePass(ResetPasswdUserModel resetPasswdUser);

        bool Delete(int id);
    }
}
