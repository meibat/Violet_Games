using VioletGames.Models;

namespace VioletGames.Data.Helper
{
    public interface ISessionUser
    {
        void CreateSessionUser(UsuarioModel usuario);
        void RemoveSessionUser();
        UsuarioModel SeachSessionUser();
    }
}
