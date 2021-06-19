using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unica.Models;
using Unica.Data;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Unica.Controllers
{
    public class FuncionarioController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View(new FuncionarioViewModel());
        }

        [HttpPost]
        public IActionResult Login(FuncionarioViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var data = new FuncionarioData())
            {
                var user = data.ReadForLogin(model.Usuario, model.Senha);

                if (user == null)
                {
                    ViewBag.Message = "Email e/ou senha incorretos!";
                    return View(model);
                }

                HttpContext.Session.SetString("user", JsonSerializer.Serialize<Funcionario>(user));

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Cliente");

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Funcionario funcionario)
        {
            //O ModeState é uma propriedade da classe Controller e pode ser
            //acessada a partir das classes que herdam de System.Web.Mvc.Controller.
            //Ele representa uma coleção de pares nome/valor que são submetidos
            //ao servidor durante o POST e também contém uma coleção de mensagens
            //de erros para cada calor submetido
            funcionario.Status = 1;
            if (!ModelState.IsValid)
            {
                return View(funcionario);
            }

            using (var data = new FuncionarioData())
                data.Create(funcionario);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using (var data = new FuncionarioData())
                return View(data.ReadById(id));
        }

        [HttpPost]
        public IActionResult Update(Funcionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                return View(funcionario);
            }

            using (var data = new FuncionarioData())
                data.Update(funcionario);

            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Funcionario");

        }
    }
}
