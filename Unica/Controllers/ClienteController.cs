﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unica.Models;
using Unica.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Unica.Controllers
{
    public class ClienteController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            using (var data = new ClienteData())
                return View(data.Read());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            cliente.Status = 1;
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            using (var data = new ClienteData())
                data.Create(cliente);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using (var data = new ClienteData())
                return View(data.ReadById(id));
        }

        [HttpPost]
        public IActionResult Update(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            using (var data = new ClienteData())
                data.Update(cliente);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            using (var data = new ClienteData())
                data.Delete(id);

            return RedirectToAction("Index");
        }


    }
}
