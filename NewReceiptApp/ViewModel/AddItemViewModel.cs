using System.Collections.Generic;
using NewReceiptApp.Models;

namespace NewReceiptApp.ViewModels
{
    public class AddItemViewModel
    {
        public Receipt Receipt { get; set; }
        public List<Item> Items { get; set; }
    }
}
