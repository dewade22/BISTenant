using Microsoft.AspNetCore.Mvc;
using Manufacturing.Data;
using Manufacturing.Helpers;
using System.Linq;

namespace Manufacturing.Controllers.Formula
{
    public class HPPHelperController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HPPHelperController(ApplicationDbContext context)
        {
            _context = context;
        }

        public JsonResult Items(string No)
        {  
            var res = _context.Items.Where(a => a.ItemNo == No).FirstOrDefault();
            return Json(res);
        }
    }
}
