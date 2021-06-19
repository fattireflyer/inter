using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unica.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Unica.Controllers
{
    public class VeiculoController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


       

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }


    }
}
