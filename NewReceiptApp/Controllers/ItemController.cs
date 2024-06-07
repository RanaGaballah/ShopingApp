using Microsoft.AspNetCore.Mvc;
using NewReceiptApp.Services;

namespace NewReceiptApp.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _itemService.GetItemsAsync();
            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
    }
}