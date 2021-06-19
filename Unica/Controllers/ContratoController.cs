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
            return View();
        }

        [HttpPost]
        public IActionResult Create(Veiculo veiculo)
        {
            veiculo.Status = 1;
            if (!ModelState.IsValid)
            {
                return View(veiculo);
            }

            using (var data = new VeiculoData())
                data.Create(veiculo);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using (var data = new VeiculoData())
                return View(data.ReadById(id));
        }

        [HttpPost]
        public IActionResult Update(Veiculo veiculo)
        {
            if (!ModelState.IsValid)
            {
                return View(veiculo);
            }

            using (var data = new VeiculoData())
                data.Update(veiculo);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            using (var data = new VeiculoData())
                data.Delete(id);

            return RedirectToAction("Index");
        }

    }
}
