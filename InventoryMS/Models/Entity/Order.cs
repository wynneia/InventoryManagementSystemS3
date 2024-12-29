namespace InventoryMS.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderDetail { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public double TotalAmount { get; set; }
        public string OrderDate { get; set; }
    }
}
