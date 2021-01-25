using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Manufacturing.Helpers;
using Manufacturing.Models.DataTable;
using Manufacturing.Data;
using Manufacturing.Data.Entities;
using Manufacturing.Services;
using Microsoft.EntityFrameworkCore;

namespace Manufacturing.Controllers
{
    public class VendorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VendorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AuthorizedAction]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VendorList(DTParameters dtParameters)
        {
            var searchBy = dtParameters.Search?.Value;
            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            //Set Orderable
            if(dtParameters.Order != null)
            {
                orderCriteria = dtParameters.Columns[dtParameters.Order[0].Column].Data;
                orderAscendingDirection = dtParameters.Order[0].Dir.ToString().ToLower() == "desc";
            }
            else
            {
                orderCriteria = "VendorNo";
                orderAscendingDirection = true;
            }

            var result = _context.Vendors.Select(vendors => new Vendors
            {
                VendorId = vendors.VendorId,
                VendorNo = vendors.VendorNo,
                VendorName = vendors.VendorName,
                VendorAddress = vendors.VendorAddress,
                VendorContact = vendors.VendorContact
            }).ToList();

            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.VendorNo != null && r.VendorNo.ToLower().Contains(searchBy.ToLower()) ||
                r.VendorName != null && r.VendorName.ToLower().Contains(searchBy.ToLower()) || 
                r.VendorAddress != null && r.VendorAddress.ToLower().Contains(searchBy.ToLower()) ||
                r.VendorContact != null && r.VendorContact.ToLower().Contains(searchBy.ToLower())
                ).ToList();
            }
            //Ordering
            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtension.Order.Asc).ToList() : result.AsQueryable().OrderByDynamic(orderCriteria, LinqExtension.Order.Desc).ToList();

            var filteredResultsCount = result.Count();
            var totalResultsCount = await _context.Vendors.CountAsync();

            return Json(new
            {
                draw = dtParameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
           .Skip(dtParameters.Start)
           .Take(dtParameters.Length)
           .ToList()
            });
        }
    }
}