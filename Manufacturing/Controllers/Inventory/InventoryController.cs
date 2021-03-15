using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Manufacturing.Controllers.Inventory
{
    public class InventoryController : Controller
    {
        public IActionResult Movement()
        {
            return View();
        }
    }
}