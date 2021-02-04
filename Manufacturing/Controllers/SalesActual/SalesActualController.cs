using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manufacturing.Helpers;

namespace Manufacturing.Controllers
{
    public class SalesActualController : Controller
    {
        [AuthorizedAction]
        public IActionResult Monthly()
        {
            return View();
        }
    }
}
