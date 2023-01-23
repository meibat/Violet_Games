using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;

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
}
