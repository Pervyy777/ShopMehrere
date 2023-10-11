using System.ComponentModel.DataAnnotations;

namespace ShopMehrere.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "имя")]
        public string? Name { get; set; }

        [Display(Name = "Почта")]
        [Required(ErrorMessage = "Не указана почта")]
        public string? Email { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан пароль")]
        public string? Password { get; set; }

        /*
        [Required(ErrorMessage = "Не указана дата рождения")]
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }*/

        public int? RoleId { get; set; }
        public Role? Role { get; set; }

        public int BasketId { get; set; }
        public Basket? Basket { get; set; }
    }
}
