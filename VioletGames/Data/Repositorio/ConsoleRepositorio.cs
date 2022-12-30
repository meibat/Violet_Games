﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;

namespace VioletGames.Data.Repositorio
{
    public class ConsoleRepositorio : IConsoleRepositorio
    {
        //Extrai variavel bancoContext
        private readonly BancoContent _bancoContent;

        public ConsoleRepositorio(BancoContent bancoContent)
        {
            //injecao de dependencia
            _bancoContent = bancoContent;
        }

        public ConsoleModel Create(ConsoleModel console)
        {
            _bancoContent.Consoles.Add(console);
            _bancoContent.SaveChanges();

            return console;
        }

        public bool Delete(int id)
        {
            ConsoleModel consoleDB = ListForID(id);

            if (consoleDB == null) throw new System.Exception("Erro na deleção do Funcionario");

            _bancoContent.Consoles.Remove(consoleDB);
            _bancoContent.SaveChanges();

            return true;
        }

        public ConsoleModel ListForID(int id)
        {
            return _bancoContent.Consoles.FirstOrDefault(x => x.Id == id);
        }

        public List<ConsoleModel> SearchAll()
        {
            return _bancoContent.Consoles.ToList();
        }

        public ConsoleModel Update(ConsoleModel console)
        {
            ConsoleModel ConsoleDB = ListForID(console.Id);

            if (ConsoleDB == null) throw new System.Exception("Erro na atualização do Cliente");

            ConsoleDB.Name = console.Name;
            ConsoleDB.Marking = console.Marking;
            ConsoleDB.PriceHour = console.PriceHour;

            //_bancoContent.Clientes.Update(ConsoleDB);
            _bancoContent.SaveChanges();

            return ConsoleDB;
        }
    }
}

