using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unica.Models;
using Unica.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Unica.Controllers
{
    public class ContratoController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            using (var data = new ContratoData())
                return View(data.Read());
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<Veiculo> veiculos;
            using (var data = new VeiculoData())
            {
                veiculos = data.Read();
            }

            List<Cliente> clientes;
            using (var data = new ClienteData())
            {
                clientes = data.Read();
            }

            ViewBag.Veiculos = veiculos.FindAll(value => value.Status == 1);
            ViewBag.Clientes = clientes;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contrato Contrato)
        {
            Contrato.Status = 1;
            if (!ModelState.IsValid)
            {
                return View(Contrato);
            }

            using (var data = new ContratoData())
                data.Create(Contrato);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            List<Veiculo> veiculos;
            using (var data = new VeiculoData())
            {
                veiculos = data.Read();
            }

            List<Cliente> clientes;
            using (var data = new ClienteData())
            {
                clientes = data.Read();
            }

            ViewBag.Veiculos = veiculos;
            ViewBag.Clientes = clientes;
            using (var data = new ContratoData())
                return View(data.Read(id));
        }

        [HttpPost]
        public IActionResult Update(Contrato contrato)
        {
            if (!ModelState.IsValid)
            {
                return View(contrato);
            }

            using (var data = new ContratoData())
                data.Update(contrato);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            using (var data = new ContratoData())
                data.Delete(id);

            return RedirectToAction("Index");
        }

        public IActionResult Finalizar(int id)
        {
            using (var data = new ContratoData())
                data.Finalizar(id);

            return RedirectToAction("Index");
        }

    }
}
