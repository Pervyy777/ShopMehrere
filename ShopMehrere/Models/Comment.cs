namespace ShopMehrere.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Text { get; set; }

        public int? SenderId { get; set; }
        public User? Sender { get; set; } 

        //public List<Star> Stars { get; set; } = new();
    }
}
