namespace NewReceiptApp.Models
{
    public class Transaction
    {

     
       
        public int Id { get; set; }
        public int QuantityPurchased { get; set; } // Quantity of the item purchased in this transaction
        public decimal Amount { get; set; } // Amount for this transaction (price * quantity purchased)


        // Foreign key
        public int ReceiptId { get; set; }
        public Receipt Receipt { get; set; }

        // Foreign key
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
