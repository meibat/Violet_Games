using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;
using VioletGames.Data.Repositorio;
using VioletGames.Data.Helper;

namespace VioletGames.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessionUser _session;
        
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessionUser session)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _session = session;
        }
        
        public IActionResult Index()
        {
            //Valida se existe sessão
            if (_session.SeachSessionUser() != null) return RedirectToAction("Index", "Dashboard");

            return View();
        }

        public IActionResult Exit()
        {
            _session.RemoveSessionUser();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Enter(LoginModel loginModel)
        {
            try {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.SeachForLogin(loginModel.Login);

                    if (usuario.Login != null)
                    {
                        if (usuario.PasswdValid(loginModel.Passwd))
                        {
                            //Cria sessão do usuário 
                            _session.CreateSessionUser(usuario);
                            return RedirectToAction("Index", "Dashboard");
                        }
                    }
                    TempData["MessagemError"] = $"Usuário ou Senha Inválido(s)! Tente novamente.";
                }
                return View("Index");
             }
            catch(Exception)
            {
                TempData["MessagemError"] = $"Não foi possível realizar seu Login! Tente novamente.";
                return RedirectToAction("Index");
            }
        }
    }
}
