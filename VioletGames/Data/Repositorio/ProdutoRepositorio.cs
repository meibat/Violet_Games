﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;

namespace VioletGames.Data.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        //Extrai variavel bancoContext
        private readonly BancoContent _bancoContent;

        public ProdutoRepositorio(BancoContent bancoContent)
        {
            //injecao de dependencia
            _bancoContent = bancoContent;
        }

        public ProdutoModel Create(ProdutoModel produto)
        {
            _bancoContent.Produtos.Add(produto);
            _bancoContent.SaveChanges();

            return produto;
        }

        public bool Delete(int id)
        {
            ProdutoModel produtoDB = ListForID(id);

            if (produtoDB == null) throw new System.Exception("Erro na deleção do Funcionario");

            _bancoContent.Produtos.Remove(produtoDB);
            _bancoContent.SaveChanges();

            return true;
        }

        public ProdutoModel ListForID(int id)
        {
            return _bancoContent.Produtos.FirstOrDefault(x => x.Id == id);
        }

        public List<ProdutoModel> SearchAll()
        {
            return _bancoContent.Produtos.ToList();
        }

        public ProdutoModel Update(ProdutoModel produto)
        {
            ProdutoModel produtoDB = ListForID(produto.Id);

            if (produtoDB == null) throw new System.Exception("Erro na atualização do Funcionario");

            produtoDB.Name = produto.Name;
            produtoDB.PriceUnity = produto.PriceUnity;
            produtoDB.QtdAvailable = produto.QtdAvailable;

            _bancoContent.Produtos.Update(produtoDB);
            _bancoContent.SaveChanges();

            return produtoDB;
        }
    }
}