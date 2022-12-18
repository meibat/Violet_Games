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

        UsuarioModel ListForID(int id);

        List<UsuarioModel> SearchAll();

        UsuarioModel Create(UsuarioModel usuario);

        UsuarioModel Update(UsuarioModel usuario);

        bool Delete(int id);
    }
}
