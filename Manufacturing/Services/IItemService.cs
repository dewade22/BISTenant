using Manufacturing.Data.Entities;
using System.Threading.Tasks;

namespace Manufacturing.Services
{
    public interface IItemService
    {
        Task<Items[]> GetIncompleteItemsAsync();
    }
}
