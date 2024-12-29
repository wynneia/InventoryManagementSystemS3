namespace InventoryMS.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TransactionDate { get; set; }
        public double Value { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
    }
}