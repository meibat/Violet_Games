﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VioletGames.Data.Filters;
using VioletGames.Models;
using VioletGames.Repositorio;

namespace VioletGames.Controllers
{
    [PageUserLogin]
    [PageUserAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Usuários";
            List<UsuarioModel> usuarios = _usuarioRepositorio.SearchAll();

            return View(usuarios);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Usuários";
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Usuários";
            UsuarioModel usuario = _usuarioRepositorio.ListForID(id);
            return View(usuario);
        }

        public IActionResult DeleteConfirm(int id)
        {
            ViewData["Title"] = "Usuários";
            UsuarioModel usuario = _usuarioRepositorio.ListForID(id);

            return View(usuario);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = _usuarioRepositorio.Delete(id);

                if (deleted)
                {
                    TempData["MessagemSucess"] = "Deletado com sucesso!";
                }
                else
                {
                    TempData["MessagemError"] = $"Não foi possível deletar o cadastro! Tente novamente.";
                }
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível deletar o cadastro! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }


        //Métodos Post
        [HttpPost]
        public IActionResult Create(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Create(usuario);
                    TempData["MessagemSucess"] = "Cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível efetuar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(UsuarioWithoutModel usuarioWithoutModel)
        {
            try
            {
                UsuarioModel usuario = null;

                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioWithoutModel.Id,
                        Name = usuarioWithoutModel.Name,
                        Login = usuarioWithoutModel.Login,
                        Email = usuarioWithoutModel.Email,
                        Perfil = usuarioWithoutModel.Perfil,
                    };

                    _usuarioRepositorio.Update(usuario);
                    TempData["MessagemSucess"] = "Cadastro editado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível editar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
