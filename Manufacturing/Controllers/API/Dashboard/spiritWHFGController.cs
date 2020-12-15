using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Manufacturing.Data;

namespace Manufacturing.Controllers.API.Dashboard
{
    [Route("api/Dashboard/spiritWHFG")]
    [ApiController]
    public class spiritWHFGController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public spiritWHFGController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult getSpirit(string tenant)
        {
            if (tenant == "Balimoon")
            {
                var result = (from itemLedger in _context.ItemLedgerEntry
                              join items in _context.Items
                              on itemLedger.ItemNo equals items.ItemNo
                              where itemLedger.LocationCode == "WHFG" && items.InventoryPostingGroup == "FGInTrans"
                              group itemLedger by new { items.ProductGroupCode, items.Attrib1Code } into res
                              select new { res.Key.ProductGroupCode, res.Key.Attrib1Code, Quantity = res.Sum(a => a.Quantity) }).ToList();
                return new JsonResult(result);
            }
            else
            {
                var result = (from itemledger in _context.ItemLedgerEntry
                              join item in _context.Items
                              on itemledger.ItemNo equals item.ItemNo
                              group itemledger by new { item.ItemCategoryCode, item.DimensionValue01Code } into res
                              select new { res.Key.ItemCategoryCode, res.Key.DimensionValue01Code, Quantity = res.Sum(a => a.Quantity) }).ToList();
                return new JsonResult(result);
            }
        }
    }
}