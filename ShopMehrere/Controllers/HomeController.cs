using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMehrere.Models;
using System.Data;
using System.Diagnostics;

namespace ShopMehrere.Controllers
{
    public class HomeController : Controller
    {
        UsersContext db;

        private readonly ILogger<HomeController> _logger;
        public HomeController(UsersContext dbcontext, ILogger<HomeController> logger)
        {
            this.db = dbcontext;
            _logger = logger;
            if (!db.Users.Any())
            {
                Role adminRole = new Role { Name = "admin" };
                Role userRole = new Role { Name = "user" };
                db.Roles.AddRange(userRole, adminRole);

                Category category = new Category { Name = "technik" };
                Category category1 = new Category { Name = "eat" };
                db.Categorys.AddRange(category, category1);

                Product product = new Product { Name = "computer", Category = category, Description = "the best computer with a lot of cores", Price = 100 };
                Product product1 = new Product { Name = "poop", Category = category1, Description = "The best eat in the world", Price = 10 };
                db.Products.AddRange(product1, product1);

                Basket b1 = new Basket { Products = new List<Product> { product, product1 } };
                Basket b2 = new Basket { Products = new List<Product> { product } };
                db.Baskets.AddRange(b1, b2);

                User user1 = new User { Name = "Олег Васильев", Basket = b1, Email = "loha@gmail.com", Password = "qwerty", Role = userRole };

                User user2 = new User { Name = "Андрей Потап", Basket = b2, Email = "lol@gmail.com", Password = "qwerty", Role = adminRole };
                db.Users.AddRange(user1, user2);
                db.SaveChanges();
            }
        }

        public async Task<IActionResult> Index(string name, int product = 0, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 3;

            //фильтрация
            IQueryable<Product> products = db.Products.Include(x => x.Category);

            if (product != 0)
            {
                products = products.Where(p => p.CategoryId == product);
            }
            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name!.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case SortState.PriceAsc:
                    products = products.OrderBy(s => s.Price);
                    break;
                case SortState.PriceDesc:
                    products = products.OrderByDescending(s => s.Price);
                    break;
                case SortState.CategoryAsc:
                    products = products.OrderBy(s => s.Category!.Name);
                    break;
                case SortState.CategoryDesc:
                    products = products.OrderByDescending(s => s.Category!.Name);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }

            // пагинация
            var count = await products.CountAsync();
            var items = await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new FilterViewModel(db.Categorys.ToList(), product, name),
                new SortViewModel(sortOrder)
            );
            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult GitHub()
        {
            return Redirect("https://github.com/Pervyy777");
        }
        public IActionResult Telegram()
        {
            return Redirect("https://t.me/Werds0777");
        }
        [Authorize(Roles = "admin, user")]
        public IActionResult Basket() 
        {
            return View();
        }
        [Authorize(Roles = "admin, user")]
        public IActionResult UserButton()
        {
            return View();
        }

        /*
            string file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files/ProductsImages/computer.png");
        
         */
    }
}