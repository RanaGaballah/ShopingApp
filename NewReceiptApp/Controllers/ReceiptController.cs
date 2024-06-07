using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewReceiptApp.Data;
using NewReceiptApp.Models;
using NewReceiptApp.ViewModels;


namespace NewReceiptApp.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly AppDbContext _context;

        public ReceiptController(AppDbContext context)
        {
            _context = context;
        }

        // Action method to create a new receipt
        [HttpPost]
        public IActionResult AddItemToCart(int itemId)
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                return NotFound();
            }

            // Create a new receipt if none exists
            var receipt = new Receipt
            {
                DateCreated = DateTime.Now,
                Status = ReceiptStatus.Open
            };
            _context.Receipt.Add(receipt);

            // Create a new transaction
            var transaction = new Transaction
            {
                ItemId = itemId,
                ReceiptId = receipt.Id,
                QuantityPurchased = 1, // Assuming quantity is 1 for now
                Amount = item.Price
            };
            _context.Transaction.Add(transaction);

            // Save changes to the database
            _context.SaveChanges();

            // Calculate total price of all items in the receipt
            decimal totalPrice = _context.Transaction.Where(t => t.ReceiptId == receipt.Id).Sum(t => t.Amount);

            // Return receipt information
            return Json(new { itemName = item.Name, itemPrice = item.Price, totalPrice = totalPrice });
        }

        public async Task<IActionResult> CreateReceipt()
        {
            var newReceipt = new Receipt
            {
                DateCreated = DateTime.Now,
                Status = ReceiptStatus.Open
            };

            _context.Receipt.Add(newReceipt);
            await _context.SaveChangesAsync();

            return RedirectToAction("AddItemToReceipt", new { receiptId = newReceipt.Id });
        }

        // Action method to add items to the receipt (GET)
        public async Task<IActionResult> AddItemToReceipt(int receiptId)
        {
            var receipt = await _context.Receipt.FindAsync(receiptId);
            var items = await _context.Items.ToListAsync();

            return View(new AddItemViewModel { Receipt = receipt, Items = items });
        }

        // Action method to add items to the receipt (POST)
        [HttpPost]
        public async Task<IActionResult> AddItemToReceipt(int receiptId, int itemId, int quantity)
        {
            var receipt = await _context.Receipt.FindAsync(receiptId);
            var item = await _context.Items.FindAsync(itemId);

            if (receipt == null || item == null)
            {
                return NotFound();
            }

            var transaction = new Transaction
            {
                ReceiptId = receiptId,
                ItemId = itemId,
                QuantityPurchased = quantity,
                Amount = item.Price * quantity
            };

            _context.Transaction.Add(transaction);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(AddItemToReceipt), new { receiptId });
        }

        // Action method to display the total and remaining amount
        public async Task<IActionResult> DisplayTotalAndRemaining(int receiptId)
        {
            var receipt = await _context.Receipt
                .Include(r => r.Transactions)
                .FirstOrDefaultAsync(r => r.Id == receiptId);

            if (receipt == null)
            {
                return NotFound();
            }

            decimal totalPrice = receipt.Transactions.Sum(t => t.Amount);
            decimal remainingAmount = totalPrice - receipt.PaidAmount;

            var viewModel = new TotalAndRemainingViewModel
            {
                Receipt = receipt,
                TotalPrice = totalPrice,
                RemainingAmount = remainingAmount
            };

            return View(viewModel);
        }

        // Action method to enter the paid amount
        [HttpPost]
        public async Task<IActionResult> EnterPaidAmount(int receiptId, decimal paidAmount)
        {
            var receipt = await _context.Receipt.FindAsync(receiptId);

            if (receipt == null)
            {
                return NotFound();
            }

            receipt.PaidAmount = paidAmount;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(DisplayTotalAndRemaining), new { receiptId });
        }

        // Action method to complete the transaction
        public async Task<IActionResult> CompleteTransaction(int receiptId)
        {
            var receipt = await _context.Receipt.FindAsync(receiptId);

            if (receipt == null)
            {
                return NotFound();
            }

            foreach (var transaction in receipt.Transactions)
            {
                var item = await _context.Items.FindAsync(transaction.ItemId);
                if (item != null)
                {
                    item.Price -= transaction.QuantityPurchased;
                }
            }

            receipt.Status = ReceiptStatus.Closed;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
