using System.Collections.Generic;
using System.Linq;
using VioletGames.Models;

namespace VioletGames.Data.Repositorio
{
    public interface IJogoRepositorio
    {
        //metodos
        JogoModel ListForID(int id);

        JogoModel ListForName(string name);

        List<JogoModel> SearchAll();

        JogoModel Create(JogoModel jogo);

        JogoModel Update(JogoModel jogo);

        bool Delete(int id);
    }

    public class JogoRepositorio : IJogoRepositorio
    {
        //Extrai variavel bancoContext
        private readonly BancoContent _bancoContent;

        public JogoRepositorio(BancoContent bancoContent)
        {
            //injecao de dependencia
            _bancoContent = bancoContent;
        }

        public JogoModel Create(JogoModel jogo)
        {
            _bancoContent.Jogos.Add(jogo);
            _bancoContent.SaveChanges();

            return jogo;
        }

        public bool Delete(int id)
        {
            JogoModel jogoDB = ListForID(id);

            if (jogoDB == null) throw new System.Exception("Erro na deleção do Funcionario");

            _bancoContent.Jogos.Remove(jogoDB);
            _bancoContent.SaveChanges();

            return true;
        }

        public JogoModel ListForID(int id)
        {
            return _bancoContent.Jogos.FirstOrDefault(x => x.Id == id);
        }

        public JogoModel ListForName(string name)
        {
            return _bancoContent.Jogos.FirstOrDefault(x => x.Name == name);
        }

        public List<JogoModel> SearchAll()
        {
            return _bancoContent.Jogos.ToList();
        }

        public JogoModel Update(JogoModel jogo)
        {
            JogoModel jogoDB = ListForID(jogo.Id);

            if (jogoDB == null) throw new System.Exception("Erro na atualização do Jogo");

            jogoDB.Name = jogo.Name;
            jogoDB.CategoryConsole = jogo.CategoryConsole;
            jogoDB.PriceHour = jogo.PriceHour;
            jogoDB.StatusJogo = jogo.StatusJogo;

            _bancoContent.Jogos.Update(jogoDB);
            _bancoContent.SaveChanges();

            return jogoDB;
        }
    }
}


