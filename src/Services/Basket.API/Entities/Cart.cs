namespace Basket.API.Entities
{
    public class Cart
    {
        public string Username { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public Cart()
        { }

        public Cart(string userName)
        {
            Username = userName;
        }

        public decimal TotalPrice => Items.Sum(item => item.ItemPrice * item.Quantity);
    }
}