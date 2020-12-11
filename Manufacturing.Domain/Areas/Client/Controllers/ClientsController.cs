using Microsoft.AspNetCore.Mvc;

namespace Manufacturing.Domain.Areas.Client.Controllers
{
    public class ClientsController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
