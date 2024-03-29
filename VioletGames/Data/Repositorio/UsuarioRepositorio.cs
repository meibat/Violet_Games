﻿using System;
using System.Collections.Generic;
using System.Linq;
using VioletGames.Data;
using VioletGames.Models;

namespace VioletGames.Repositorio
{
    public interface IUsuarioRepositorio
    {
        //metodos
        UsuarioModel SeachForLogin(string login);
        UsuarioModel SeachForLoginAndEmail(string login, string email);

        UsuarioModel ListForID(int id);

        List<UsuarioModel> SearchAll();

        UsuarioModel Create(UsuarioModel usuario);

        UsuarioModel Update(UsuarioModel usuario);

        UsuarioModel UpdatePass(ResetPasswdUserModel resetPasswdUser);

        bool Delete(int id);
    }

    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        //Extrai variavel bancoContext
        private readonly BancoContent _bancoContent;

        public UsuarioRepositorio(BancoContent bancoContent)
        {
            //injecao de dependencia
            _bancoContent = bancoContent;
        }

        //busca os dados pelo id
        public UsuarioModel ListForID(int id)
        {
            return _bancoContent.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        //busca todos no banco
        public List<UsuarioModel> SearchAll()
        {
            return _bancoContent.Usuarios.ToList();
        }

        //gravar no banco
        public UsuarioModel Create(UsuarioModel usuario)
        {
            usuario.DateSingIn = DateTime.Now;
            usuario.SetPasswdHash();

            _bancoContent.Usuarios.Add(usuario);
            _bancoContent.SaveChanges();

            return usuario;
        }

        public UsuarioModel Update(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListForID(usuario.Id);

            if (usuarioDB == null) throw new System.Exception("Erro na atualização do Funcionario");

            usuarioDB.Name = usuario.Name;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DateRefresh = DateTime.Now;

            _bancoContent.Usuarios.Update(usuarioDB);
            _bancoContent.SaveChanges();

            return usuarioDB;
        }

        public bool Delete(int id)
        {
            UsuarioModel usuarioDB = ListForID(id);

            if (usuarioDB == null) throw new System.Exception("Erro na deleção do Funcionario");

            _bancoContent.Usuarios.Remove(usuarioDB);
            _bancoContent.SaveChanges();

            return true;
        }

        UsuarioModel IUsuarioRepositorio.SeachForLogin(string login)
        {
            return _bancoContent.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        //Reset 
        public UsuarioModel SeachForLoginAndEmail(string login, string email)
        {
            return _bancoContent.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper() && x.Email.ToUpper() == email.ToUpper());
        }

        public UsuarioModel UpdatePass(ResetPasswdUserModel resetPasswdUser)
        {
            UsuarioModel usuarioDB = ListForID(resetPasswdUser.Id);
            if(usuarioDB == null) throw new Exception("Erro: o usuário não foi encontrado.");

            if(!usuarioDB.PasswdValid(resetPasswdUser.Passwd)) throw new Exception("Erro: Senha atual não confere.");

            if(usuarioDB.PasswdValid(resetPasswdUser.NewPasswd)) throw new Exception("Erro: Senha nova não pode ser a mesma que a atual.");

            usuarioDB.SetNewPasswd(resetPasswdUser.NewPasswd);
            usuarioDB.DateRefresh = DateTime.Now;

            _bancoContent.Usuarios.Update(usuarioDB);
            _bancoContent.SaveChanges();

            return usuarioDB;
        }
    }
}
