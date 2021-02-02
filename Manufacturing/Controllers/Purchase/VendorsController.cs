using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Manufacturing.Helpers;
using Manufacturing.Data;
using Manufacturing.Models.Vendors;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var data =  (from vendor in _context.Vendors
                        select new VendorViewModel
                        {
                            VendorId = vendor.VendorId,
                            VendorNo = vendor.VendorNo,
                            VendorName = vendor.VendorName,
                            VendorAddress = vendor.VendorAddress,
                            VendorContact = vendor.VendorContact
                        }).OrderByDescending(sort => sort.VendorId).ToList();
           return View(data);
        }

        [AuthorizedAction]
        public IActionResult Detil(int Id)
        {
            
            List<Manufacturing.Data.Entities.CountryRegion> Countries = _context.CountryRegion.ToList();
            ViewBag.Countries = new SelectList(Countries, "Code", "Name");
            var data = _context.Vendors.Where(vendor => vendor.VendorId == Id).SingleOrDefault();
            return View(data);
        }
    }
}