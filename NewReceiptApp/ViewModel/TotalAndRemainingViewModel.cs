using NewReceiptApp.Models;

namespace NewReceiptApp.ViewModels
{
    public class TotalAndRemainingViewModel
    {
        public Receipt Receipt { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal RemainingAmount { get; set; }
    }
}
