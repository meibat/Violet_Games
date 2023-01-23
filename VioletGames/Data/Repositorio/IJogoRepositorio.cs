using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;

namespace VioletGames.Data.Repositorio
{
    public interface IJogoRepositorio
    {
        //metodos
        JogoModel ListForID(int id);

        List<JogoModel> SearchAll();

        JogoModel Create(JogoModel jogo);

        JogoModel Update(JogoModel jogo);

        bool Delete(int id);
    }
}
