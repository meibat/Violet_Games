using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;

namespace VioletGames.Data.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        //Extrai variavel bancoContext
        private readonly BancoContent _bancoContent;

        public ClienteRepositorio(BancoContent bancoContent)
        {
            //injecao de dependencia
            _bancoContent = bancoContent;
        }

        //busca os dados pelo id
        public ClienteModel ListForIDClient(int id)
        {
            return _bancoContent.Clientes.FirstOrDefault(x => x.Id == id);
        }

        public ContatoModel ListForIDContact(int id)
        {
            return _bancoContent.Contatos.FirstOrDefault(x => x.Id == id);
        }

        //busca todos no banco
        public List<ClienteModel> SearchAll()
        {
            return _bancoContent.Clientes.ToList();
        }

        //gravar no banco
        public ClienteModel Create(ClienteModel cliente)
        {
            _bancoContent.Clientes.Add(cliente);
            _bancoContent.SaveChanges();

            return cliente;
        }

        public ClienteModel Update(ClienteModel cliente, ContatoModel contato)
        {
            ClienteModel ClienteDB = ListForIDClient(cliente.Id);
            int numID =  Convert.ToInt16(cliente.Contato);

            if (ClienteDB == null) throw new System.Exception("Erro na atualização do Cliente");

            ClienteDB.Name = cliente.Name;
            ClienteDB.CPF = cliente.CPF;
            ClienteDB.DateBirthday = cliente.DateBirthday;

            ContatoModel ContatoDB = ListForIDContact(numID);

            if (ContatoDB == null) throw new System.Exception("Erro na atualização do Contato");

            ContatoDB.Phone = contato.Phone;
            ContatoDB.Address = contato.Address;

            _bancoContent.Clientes.Update(ClienteDB);
            _bancoContent.Contatos.Update(ContatoDB);
            _bancoContent.SaveChanges();

            return ClienteDB;
        }


        public bool Delete(int id)
        {
            ClienteModel ClienteDB = ListForIDClient(id);

            if (ClienteDB == null) throw new System.Exception("Erro na deleção do Cliente");

            _bancoContent.Clientes.Remove(ClienteDB);
            _bancoContent.SaveChanges();

            return true;
        }
    }
}