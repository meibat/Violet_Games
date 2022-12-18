using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using VioletGames.Models;

namespace VioletGames.Data.Helper
{
    public class SessionUser : ISessionUser
    {
        private readonly IHttpContextAccessor _httpContext;

        public SessionUser(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public void CreateSessionUser(UsuarioModel usuario)
        {
            String userValue = JsonConvert.SerializeObject(usuario);

            _httpContext.HttpContext.Session.SetString("SessionSinginUser", userValue);
        }

        public void RemoveSessionUser()
        {
            _httpContext.HttpContext.Session.Remove("SessionSinginUser");
        }

        public UsuarioModel SeachSessionUser()
        {
            String sessionUser = _httpContext.HttpContext.Session.GetString("SessionSinginUser");

            if (string.IsNullOrEmpty(sessionUser)) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessionUser);
        }
    }
}
