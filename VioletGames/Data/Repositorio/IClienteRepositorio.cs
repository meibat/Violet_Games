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

        ContatoModel ListForIDContact(int id);

        List<ClienteModel> SearchAll();

        ClienteModel Create(ClienteModel cliente);

        ClienteModel Update(ClienteModel cliente, ContatoModel contato);

        bool Delete(int id);
    }
}
