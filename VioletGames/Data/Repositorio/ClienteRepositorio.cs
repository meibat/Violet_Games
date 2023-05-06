using System;
using System.Collections.Generic;
using System.Linq;
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

        PlanoModel ListPlanForClient(ClienteModel cliente);

        PlanoModel ListPlanForID(int id);
    }

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

        //busca todos no banco
        public List<ClienteModel> SearchAll()
        {
            return _bancoContent.Clientes.ToList();
        }

        //gravar no banco
        public ClienteModel Create(ClienteModel cliente)
        {
            _bancoContent.Clientes.Add(cliente);

            if(cliente.Plano != Enums.Plan.Free)
            {
                PlanoModel plano = new PlanoModel();
                plano.CPF = cliente.CPF;
                plano.Plano = cliente.Plano;
                plano.payment = cliente.payment;
                plano.PlanDay = cliente.PlanDay;

                _bancoContent.Planos.Add(plano);
            }
            
            _bancoContent.SaveChanges();

            return cliente;
        }

        public ClienteModel Update(ClienteModel cliente)
        {
            ClienteModel ClienteDB = ListForIDClient(cliente.Id);

            if (ClienteDB == null) throw new System.Exception("Erro na atualização do Cliente");

            ClienteDB.Name = cliente.Name;
            ClienteDB.CPF = cliente.CPF;
            ClienteDB.DateBirthday = cliente.DateBirthday;

            //contato
            ClienteDB.State = cliente.State;
            ClienteDB.City = cliente.City;
            ClienteDB.Address = cliente.Address;
            ClienteDB.Number = cliente.Number;
            ClienteDB.CEP = cliente.CEP;
            ClienteDB.Email = cliente.Email;
            ClienteDB.Phone = cliente.Phone;


            if (cliente.Plano != Enums.Plan.Free && cliente.Plano != ClienteDB.Plano)
            {
                PlanoModel plano = new PlanoModel();

                plano.CPF = ClienteDB.CPF;
                plano.Plano = ClienteDB.Plano = cliente.Plano; ;
                plano.payment = ClienteDB.payment = Enums.StatusPayment.Pendente;
                plano.PlanDay = ClienteDB.PlanDay = cliente.PlanDay;

                _bancoContent.Planos.Add(plano);
            }
            else
            {
                ClienteDB.Plano = cliente.Plano; ;
                ClienteDB.payment = Enums.StatusPayment.Pago;
            }

            _bancoContent.Clientes.Update(ClienteDB);
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

        public bool isClient(string cpf)
        {
                return _bancoContent.Clientes.Any(x => x.CPF == cpf);
        }

        public ClienteModel ListForCPF(string cpf)
        {
            return _bancoContent.Clientes.FirstOrDefault(x => x.CPF == cpf);
        }

        public PlanoModel ListPlanForClient(ClienteModel cliente)
        {
            var query = from Plano in _bancoContent.Planos 
                        where Plano.CPF == cliente.CPF && Plano.payment == Enums.StatusPayment.Pendente 
                        select Plano;

            PlanoModel plano = query.FirstOrDefault<PlanoModel>();
  
            return plano;
        }

        public PlanoModel ListPlanForID(int id)
        {
            PlanoModel plano = _bancoContent.Planos.FirstOrDefault(x => x.Id == id);

            Console.WriteLine("ClienteRepo - ListPlanForID");

            return plano;
        }
    }
}