using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manufacturing.Models.Items;
using Manufacturing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Manufacturing.Data;


namespace Manufacturing.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IItemService _ItemService;
        public ItemController(IItemService ItemService, ApplicationDbContext applicationDbContext)
        {
            _ItemService = ItemService;
            _applicationDbContext = applicationDbContext;
        }
        public ActionResult Index()
        {
            var data = (from items in _applicationDbContext.Items
                        select new itemTableVM
                        {
                            ItemNo = items.ItemNo,
                            Description = items.Description,
                            BaseUnitofMeasure = items.BaseUnitofMeasure,
                            ItemCategoryCode = items.ItemCategoryCode,
                            ProductGroupCode = items.ProductGroupCode
                        }).ToList();
            return View(data);
        }
    }
}
