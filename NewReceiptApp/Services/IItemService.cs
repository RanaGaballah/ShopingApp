using NewReceiptApp.Models;

namespace NewReceiptApp.Services
{
    public interface IItemService
    {
        Task<List<Item>> GetItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);
        Task<List<Item>> GetAllItemsAsync();
    }
}