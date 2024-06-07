namespace NewReceiptApp.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public ReceiptStatus Status { get; set; }
        public decimal PaidAmount { get; set; }

        // Navigation property
        public ICollection<Transaction> Transactions { get; set; }
    }

    public enum ReceiptStatus
    {
        Open,
        Closed
    }
}
