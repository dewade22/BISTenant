using Manufacturing.Data;
using Manufacturing.Data.Entities;
using Manufacturing.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manufacturing.Services
{
    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext _context;

        public ItemService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Item[]> GetIncompleteItemsAsync()
        {
            return await _context.Items
            //.Where(x => x.RowStatus == 0)
            .ToArrayAsync();
        }
    }    
}
