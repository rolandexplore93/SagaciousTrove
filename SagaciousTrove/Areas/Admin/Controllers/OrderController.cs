using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SagaciousTrove.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET:
        public IActionResult Index()
        {
            return View();
        }
    }
}

