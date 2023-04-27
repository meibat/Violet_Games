﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VioletGames.Models;
using VioletGames.Util.Validator;
using VioletGames.Data.Filters;
using VioletGames.Repositorio;

namespace VioletGames.Controllers
{
    [PageUserLogin]
    [PageUserAdmin]
    public class FuncionarioController : Controller
    {
        //Extrai variavel FuncionarioRepositorio
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;

        public FuncionarioController(IFuncionarioRepositorio funcionarioRepositorio)
        {
            //injeta para dentro de FuncionarioController
            _funcionarioRepositorio = funcionarioRepositorio;
        }

        //Métodos Get
        public IActionResult Index()
        {
            //lista a busca feita no banco
            ViewData["Title"] = "Funcionários";
            List<FuncionarioModel> funcionarios = _funcionarioRepositorio.SearchAll();

            return View(funcionarios);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Funcionários";
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Funcionários";
            FuncionarioModel funcionario = _funcionarioRepositorio.ListForIDEmployee(id);
            return View(funcionario);
        }

        public IActionResult DeleteConfirm(int id)
        {
            ViewData["Title"] = "Funcionários";
            FuncionarioModel funcionario = _funcionarioRepositorio.ListForIDEmployee(id);

            return View(funcionario);
        }

        public IActionResult Delete(int id) 
        {
            try
            {
                bool deleted = _funcionarioRepositorio.Delete(id);

                if (deleted)
                {
                    TempData["MessagemSucess"] = "Funcionário deletado com sucesso!";
                }
                else{
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
        public IActionResult Create(FuncionarioModel funcionario) 
        {
            try{
                if (ModelState.IsValid)
                {
                    if (!Validator.IsCPF(funcionario.CPF))
                    { 
                        TempData["MessagemError"] = "CPF informado Inválido!";
                        return View(funcionario);
                    }
                    if(!Validator.IsPhone(funcionario.Phone))
                    {
                        TempData["MessagemError"] = "Telefone informado Inválido!";
                        return View(funcionario);
                    }

                    if(!Validator.IsDateValid(funcionario.DateBirthday))
                    {
                        TempData["MessagemError"] = "Data informada Inválida!";
                        return View(funcionario);
                    }

                    _funcionarioRepositorio.Create(funcionario);
                    TempData["MessagemSucess"] = "Funcionário cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(funcionario);
            }
            catch (System.Exception erro){
                TempData["MessagemError"] = $"Não foi possível efetuar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(FuncionarioModel funcionario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!Validator.IsCPF(funcionario.CPF))
                    {
                        TempData["MessagemError"] = "CPF informado Inválido!";
                        return View(funcionario);
                    }
                    if (!Validator.IsPhone(funcionario.Phone))
                    {
                        TempData["MessagemError"] = "Telefone informado Inválido!";
                        return View(funcionario);
                    }

                    _funcionarioRepositorio.Update(funcionario);
                    TempData["MessagemSucess"] = "Cadastro editado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(funcionario);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível editar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
