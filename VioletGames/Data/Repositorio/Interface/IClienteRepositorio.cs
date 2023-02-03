using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;

namespace VioletGames.Data.Repositorio
{
    public interface IClienteRepositorio
    {
        //metodos
        ClienteModel ListForIDClient(int id);
        
        ClienteModel ListForCPF(string cpf);

        List<ClienteModel> SearchAll();

        bool isClient(string cpf);

        ClienteModel Create(ClienteModel cliente);

        ClienteModel Update(ClienteModel cliente);

        bool Delete(int id);
    }
}
