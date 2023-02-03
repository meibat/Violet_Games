using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;
using VioletGames.Util.Validator;

namespace VioletGames.Data.Repositorio
{
    public class AgendamentoRepositorio : IAgendamentoRepositorio
    {
        //Extrai variavel bancoContext
        private readonly BancoContent _bancoContent;

        public AgendamentoRepositorio(BancoContent bancoContent)
        {
            //injecao de dependencia
            _bancoContent = bancoContent;
        }

        public AgendamentoModel Create(AgendamentoModel Agendamento)
        {
            AgendamentoModel agenda = new AgendamentoModel();


                    agenda.DateSchedule = Agendamento.DateSchedule;
                    agenda.DateEnter = Agendamento.DateEnter;
                    agenda.DateClose = Agendamento.DateClose;
                    agenda.Payment = Agendamento.Payment;
                    agenda.LoginUser = Agendamento.LoginUser;
                    agenda.TotalValue = Agendamento.TotalValue;
                    agenda.NameGameOrConsole = Agendamento.NameGameOrConsole;
                    agenda.CPFClient = Agendamento.CPFClient;
                    agenda.Category = Agendamento.Category;
            agenda.NameClient = Agendamento.NameClient;

                    _bancoContent.Agendamentos.Add(agenda);
                    _bancoContent.SaveChanges();

                    return Agendamento;
        }

        public bool Delete(int id)
        {
            AgendamentoModel agendaDB = ListForID(id);

            if (agendaDB == null) throw new System.Exception("Erro na deleção do Agendamento");

            _bancoContent.Agendamentos.Remove(agendaDB);
            _bancoContent.SaveChanges();

            return true;
        }

        public AgendamentoModel ListForID(int id)
        {
            return _bancoContent.Agendamentos.FirstOrDefault(x => x.Id == id);
        }

        public List<AgendamentoModel> SearchAll()
        {
            return _bancoContent.Agendamentos.ToList();
        }

        public AgendamentoModel Update(AgendamentoModel Agendamento)
        {
            AgendamentoModel agendaDB = ListForID(Agendamento.Id);

            if (agendaDB == null) throw new System.Exception("Erro na atualização do Cliente");

            agendaDB.LoginUser = Agendamento.LoginUser;
            agendaDB.CPFClient = Agendamento.CPFClient;
            agendaDB.NameGameOrConsole = Agendamento.NameGameOrConsole;
            agendaDB.Category = Agendamento.Category;
            agendaDB.DateSchedule = Agendamento.DateSchedule;
            agendaDB.DateEnter = Agendamento.DateEnter;
            agendaDB.DateClose = Agendamento.DateClose;
            agendaDB.TotalValue = Agendamento.TotalValue;
            agendaDB.Payment = Agendamento.Payment;
            agendaDB.NameClient = Agendamento.NameClient;

            _bancoContent.Agendamentos.Update(agendaDB);
            _bancoContent.SaveChanges();

            return agendaDB;
        }
    }
}
