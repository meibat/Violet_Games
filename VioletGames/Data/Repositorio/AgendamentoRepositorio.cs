using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;
using VioletGames.Util.Validator;

namespace VioletGames.Data.Repositorio
{
    public interface IAgendamentoRepositorio
    {
        //metodos
        AgendamentoModel ListForID(int id);

        List<AgendamentoModel> SearchAll();

        AgendamentoModel Create(AgendamentoModel Agendamento);

        AgendamentoModel Update(AgendamentoModel Agendamento);

        bool Delete(int id);
    }

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
            agenda.NameGameOrConsole = Agendamento.NameGameOrConsole;
            agenda.CPFClient = Agendamento.CPFClient;
            agenda.Category = Agendamento.Category;
            agenda.NameClient = Agendamento.NameClient;
            
            if (Agendamento.DateClose != null)
            {
                //Calcula as horas de uso e o valor a pagar
                var Hours = Agendamento.DateClose.Value.Subtract(Agendamento.DateEnter);
                agenda.TotalValue = Math.Round(Agendamento.TotalValue * Hours.TotalHours, 2);
                agenda.HourtoUse =  $"{Hours.Days} Dias {Hours.Hours}h {Hours.Minutes}min";
            }

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
            agendaDB.Payment = Agendamento.Payment;
            agendaDB.NameClient = Agendamento.NameClient;

            if (Agendamento.DateClose != null)
            {
                //Calcula as horas de uso e o valor a pagar
                var Hours = Agendamento.DateClose.Value.Subtract(Agendamento.DateEnter);
                agendaDB.TotalValue = Math.Round(Agendamento.TotalValue * Hours.TotalHours, 2);
                agendaDB.HourtoUse = $"{Hours.Days} Dias {Hours.Hours}h {Hours.Minutes}min";
            }

            _bancoContent.Agendamentos.Update(agendaDB);
            _bancoContent.SaveChanges();

            return agendaDB;
        }
    }
}
