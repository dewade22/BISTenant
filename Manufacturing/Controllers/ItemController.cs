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

        public ActionResult getItems(int? rowCount, int? current, string searchPhrase)
        {
            int records_per_page = 10;
            int start_from = 0;
            int current_page_number = 1;
            if (rowCount != null)
            {
                records_per_page = rowCount ?? default(int);
            }
            else
            {
                records_per_page = 10;
            }
            if (current != null)
            {
                current_page_number = current ?? default(int);
            }
            else
            {
                current_page_number = 1;
            }
            start_from = (current_page_number - 1) * records_per_page;

            var data = (from item in _applicationDbContext.Items
                        select new { ItemNo = item.ItemNo,
                            Description = item.Description, 
                            ItemCategoryCode = item.ItemCategoryCode, 
                            ProductGroupCode = item.ProductGroupCode,
                        BaseUnitofMeasure = item.BaseUnitofMeasure}).ToList();
            var model = data.ToList();
            if (!string.IsNullOrEmpty(searchPhrase))
            {
                model = model.Where(r => r.Description != null && r.Description.ToUpper().Contains(searchPhrase.ToUpper()) ||
                r.ItemCategoryCode != null && r.ItemCategoryCode.ToUpper().Contains(searchPhrase.ToUpper()) ||
                r.ItemNo != null && r.ItemNo.ToUpper().Contains(searchPhrase.ToUpper()) ||
                r.ProductGroupCode != null && r.ProductGroupCode.ToUpper().Contains(searchPhrase.ToUpper()) ||
                r.BaseUnitofMeasure != null && r.BaseUnitofMeasure.ToUpper().Contains(searchPhrase.ToUpper())
                ).ToList();
            }
            var filteredResultsCount = model.Count();
            var totalResultsCount = _applicationDbContext.Items.Count();
            return new JsonResult(new { current = current_page_number, rowCount = records_per_page, rows = model, total = totalResultsCount });
        }
    }
}
