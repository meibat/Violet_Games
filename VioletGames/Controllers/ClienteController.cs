using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Data.Filters;
using VioletGames.Data.Repositorio;
using VioletGames.Models;
using VioletGames.Util.Validator;

namespace VioletGames.Controllers
{
    [PageUserLogin]
    public class ClienteController : Controller
    {
        //Extrai variavel ClienteRepositorio
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            //injeta para dentro de ClienteController
            _clienteRepositorio = clienteRepositorio;
        }

        public IActionResult Index()
        {
            //lista a busca feita no banco
            ViewData["Title"] = "Clientes";
            List<ClienteModel> clientes = _clienteRepositorio.SearchAll();
            
            return View(clientes);
        }
        public IActionResult Create()
        {
            ViewData["Title"] = "Clientes";
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Clientes";
            ClienteModel cliente = _clienteRepositorio.ListForIDClient(id);
            return View(cliente);
        }

        public IActionResult DeleteConfirm(int id)
        {
            ViewData["Title"] = "Clientes";
            ClienteModel cliente = _clienteRepositorio.ListForIDClient(id);

            return View(cliente);
        }

        public IActionResult Delete(int id)
        {
            try{
                bool deleted = _clienteRepositorio.Delete(id);

                if (deleted)
                {
                    TempData["MessagemSucess"] = "Cliente deletado com sucesso!";
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

        public IActionResult PayPlan(int id)
        {
            ClienteModel cliente = _clienteRepositorio.ListForIDClient(id);

            return RedirectToAction("PayPlan", "Caixa", cliente);
        }


        //Métodos Post
        [HttpPost]
        public IActionResult Create(ClienteModel cliente)
        {
            try{
                if (ModelState.IsValid)
                {
                    if (!Validator.IsCPF(cliente.CPF))
                    {
                        TempData["MessagemError"] = "CPF informado Inválido!";
                        return View(cliente);
                    }
                    if (!Validator.IsPhone(cliente.Phone))
                    {
                        TempData["MessagemError"] = "Telefone informado Inválido!";
                        return View(cliente);
                    }

                    if(!Validator.IsDateValid(cliente.DateBirthday))
                    {
                        TempData["MessagemError"] = "Data informada Inválida!";
                        return View(cliente);
                    }

                    if (cliente.plan != Data.Enums.Plan.Free) cliente.payment = Data.Enums.StatusPayment.Pendente;
                    if (cliente.plan == Data.Enums.Plan.Free) cliente.payment = Data.Enums.StatusPayment.Pago;

                    _clienteRepositorio.Create(cliente);
                    TempData["MessagemSucess"] = "Cliente cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(cliente);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível efetuar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public IActionResult Edit(ClienteModel cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!Validator.IsCPF(cliente.CPF))
                    {
                        TempData["MessagemError"] = "CPF informado Inválido!";
                        return View(cliente);
                    }
                    if (!Validator.IsPhone(cliente.Phone))
                    {
                        TempData["MessagemError"] = "Telefone informado Inválido!";
                        return View(cliente);
                    }
                    _clienteRepositorio.Update(cliente);
                    TempData["MessagemSucess"] = "Cadastro editado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(cliente);
            }
            catch (System.Exception erro)
            {
                TempData["MessagemError"] = $"Não foi possível editar o cadastrado! Tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
