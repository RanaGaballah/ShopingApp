using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NewReceiptApp.Models;
using NewReceiptApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewReceiptApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IItemService _itemService;

        public IndexModel(ILogger<IndexModel> logger, IItemService itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }

        public List<Item> Items { get; set; }

        public async Task OnGetAsync()
        {
            Items = await _itemService.GetAllItemsAsync();
        }
    }
}
