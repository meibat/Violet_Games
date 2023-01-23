using Microsoft.EntityFrameworkCore;
using VioletGames.Models;

namespace VioletGames.Data
{
    //classe que herda da classe DbContext
    public class BancoContent : DbContext
    {
        //Configura o contexto das tabelas
        public BancoContent(DbContextOptions<BancoContent> options) : base(options)
        {

        }

        //Tabela de Contatos
        public DbSet<FuncionarioModel> Funcionarios { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<ConsoleModel> Consoles { get; set; }
        public DbSet<JogoModel> Jogos { get; set; }
        public DbSet<AgendamentoModel> Agendamentos { get; set; }
    }
}
//Após criar a conexão(String+Service), as colunas, a config da tabela e a tabela:
//No console do Nuget digite os dois comandos:
//Add-Migration CriandoTabela -context BancoContent
//Update-Database -context BancoContent
//SET IDENTITY_INSERT Usuarios ON;