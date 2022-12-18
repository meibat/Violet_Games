using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Data;
using VioletGames.Data.Repositorio;
using VioletGames.Models;

namespace VioletGames.Repositorio
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        //Extrai variavel bancoContext
        private readonly BancoContent _bancoContent;

        public FuncionarioRepositorio(BancoContent bancoContent)
        {
            //injecao de dependencia
            _bancoContent = bancoContent;
        }

        //busca os dados pelo id
        public FuncionarioModel ListForIDEmployee(int id)
        {
            return _bancoContent.Funcionarios.FirstOrDefault(x => x.Id == id);
        }

        public ContatoModel ListForIDContact(int id)
        {
            return _bancoContent.Contatos.FirstOrDefault(x => x.Id == id);
        }

        //busca todos no banco
        public List<FuncionarioModel> SearchAll()
        {
            return _bancoContent.Funcionarios.ToList();
        }

        //gravar no banco
        public FuncionarioModel Create(FuncionarioModel funcionario)
        {
            _bancoContent.Funcionarios.Add(funcionario);
            _bancoContent.SaveChanges();

            return funcionario;
        }

        public FuncionarioModel Update(FuncionarioModel funcionario, ContatoModel contato)
        {
            FuncionarioModel funcionarioDB = ListForIDEmployee(funcionario.Id);
            int numID = funcionario.Contato.Id;

            if (funcionarioDB == null) throw new System.Exception("Erro na atualização do Funcionario");

            funcionarioDB.Name = funcionario.Name;
            funcionarioDB.Email = funcionario.Email;
            funcionarioDB.CPF = funcionario.CPF;
            funcionarioDB.RG = funcionario.RG;
            funcionarioDB.Office = funcionario.Office;
            funcionarioDB.Pay = funcionario.Pay;
            funcionarioDB.DateAdmission = funcionario.DateAdmission;
            funcionarioDB.DateBirthday = funcionario.DateBirthday;

            ContatoModel ContatoDB = ListForIDContact(numID);

            if (ContatoDB == null) throw new System.Exception("Erro na atualização do Contato");

            ContatoDB.Phone = contato.Phone;
            ContatoDB.Address = contato.Address;

            _bancoContent.Funcionarios.Update(funcionarioDB);
            _bancoContent.Contatos.Update(ContatoDB);
            _bancoContent.SaveChanges();

            return funcionarioDB;
        }

        public bool Delete(int id)
        {
            FuncionarioModel funcionarioDB = ListForIDEmployee(id);

            if (funcionarioDB == null) throw new System.Exception("Erro na deleção do Funcionario");

            _bancoContent.Funcionarios.Remove(funcionarioDB);
            _bancoContent.SaveChanges();

            return true;
        }
    }
}
