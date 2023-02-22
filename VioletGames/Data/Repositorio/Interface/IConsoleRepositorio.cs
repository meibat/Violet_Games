using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;

namespace VioletGames.Data.Repositorio
{
    public interface IConsoleRepositorio
    {
        //metodos
        ConsoleModel ListForID(int id);

        ConsoleModel ListForName(string name);

        List<ConsoleModel> SearchAll();

        ConsoleModel Create(ConsoleModel console);

        ConsoleModel Update(ConsoleModel console);

        bool Delete(int id);
    }
}
