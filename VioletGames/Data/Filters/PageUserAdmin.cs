using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using VioletGames.Models;

namespace VioletGames.Data.Filters 
{
    public class PageUserAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string sessionUser = context.HttpContext.Session.GetString("SessionSinginUser");
            if (string.IsNullOrEmpty(sessionUser))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
            else
            {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessionUser);

                if(usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
                if(usuario.Perfil != Enums.PerfilEnum.Admin)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
