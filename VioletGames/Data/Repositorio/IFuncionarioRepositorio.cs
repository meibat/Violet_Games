using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;

namespace VioletGames.Data.Repositorio 
{
    public interface IFuncionarioRepositorio
    {
        //metodos
        FuncionarioModel ListForIDEmployee(int id);

        List<FuncionarioModel> SearchAll();

        FuncionarioModel Create(FuncionarioModel funcionario);

        FuncionarioModel Update(FuncionarioModel funcionario);

        bool Delete(int id);
    }
}
