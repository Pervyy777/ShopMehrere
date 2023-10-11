using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace ShopMehrere.Models
{
    public class Star
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
