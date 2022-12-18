using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;

namespace VioletGames.Data.Repositorio
{
    public interface IProdutoRepositorio
    {
        //metodos
        ProdutoModel ListForID(int id);

        List<ProdutoModel> SearchAll();

        ProdutoModel Create(ProdutoModel produto);

        ProdutoModel Update(ProdutoModel produto);

        bool Delete(int id);
    }
}
