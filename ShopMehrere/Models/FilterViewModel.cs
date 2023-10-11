using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ShopMehrere.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Category> categorys, int category, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            categorys.Insert(0, new Category { Name = "Все", Id = 0 });
            Categorys = new SelectList(categorys, "Id", "Name", category);
            SelectedCategory = category;
            SelectedName = name;
        }
        public SelectList Categorys { get; } // список компаний
        public int SelectedCategory { get; } // выбранная компания
        public string SelectedName { get; } // введенное имя
    }
}
