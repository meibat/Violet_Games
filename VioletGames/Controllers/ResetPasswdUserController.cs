using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Data.Filters;
using VioletGames.Data.Helper;
using VioletGames.Models;
using VioletGames.Repositorio;

namespace VioletGames.Controllers
{
    [PageUserLogin]
    public class ResetPasswdUserController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessionUser _session;

        public ResetPasswdUserController(IUsuarioRepositorio usuarioRepositorio, ISessionUser sessionUser)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _session = sessionUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPasswdToUser(ResetPasswdUserModel resetPasswdUserModel)
        {
            try
            {
                UsuarioModel usuarioLogin = _session.SeachSessionUser();
                resetPasswdUserModel.Id = usuarioLogin.Id;

                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.UpdatePass(resetPasswdUserModel);
                    TempData["MessagemSucess"] = "Senha alterada com sucesso!";
                    return View("Index", resetPasswdUserModel);
                }
                return View("Index", resetPasswdUserModel);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível alterar Senha! Tente novamente, detalhe do erro: {erro.Message}";
                return View("Index", resetPasswdUserModel);
            }
        }
    }
}
