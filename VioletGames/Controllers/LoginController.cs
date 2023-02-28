using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Models;
using VioletGames.Data.Helper;
using VioletGames.Util.SendEmail;
using VioletGames.Repositorio;

namespace VioletGames.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessionUser _session;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessionUser session, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _session = session;
            _email = email;
        }
        
        public IActionResult Index()
        {
            //Valida se existe sessão
            if (_session.SeachSessionUser() != null) return RedirectToAction("Index", "Dashboard");

            return View();
        }

        public IActionResult ResetPasswd()
        {
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

        [HttpPost]
        public IActionResult ResetPasswdForEmail(ResetPasswdModel resetPasswdModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.SeachForLoginAndEmail(resetPasswdModel.Login, resetPasswdModel.Email);

                    if (usuario != null)
                    {
                        string newPasswd = usuario.CreateNewPasswd();
                        string message = $"Olá, {usuario.Name}! Sua nova Senha é: {newPasswd}";

                        bool emailSent = _email.Send(usuario.Email, "Violet_Games - Nova Senha", message);

                        if (emailSent)
                        {
                            _usuarioRepositorio.Update(usuario);
                            TempData["MessagemSucess"] = "Foi enviado para seu e-mail cadastrado uma nova Senha!";
                        }
                        else
                        {
                            TempData["MessagemError"] = "Não foi possível enviar o e-mail! Tente novamente.";
                        }
                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MessagemError"] = "Não foi possível redefinir sua senha! Tente novamente.";
                }
                return View("ResetPasswd");
            }
            catch (Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível redefinir sua senha! Tente novamente. error: {erro}";
                return RedirectToAction("Index");
            }
        }
    }
}
