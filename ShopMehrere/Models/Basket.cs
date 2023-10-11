namespace ShopMehrere.Models
{
    public class Basket
    {
        public int Id { get; set; }

        public List<Product> Products { get; set; } = new();
    }
}
