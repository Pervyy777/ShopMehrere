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
                db.Categories.AddRange(category, category1);

                Product product = new Product { Name = "computer", Category = category, Description = "the best computer with a lot of cores", Price = 100 };
                Product product1 = new Product { Name = "poop", Category = category1, Description = "The best eat in the world", Price = 10 };
                db.Products.AddRange(product1, product1);

                Basket b1 = new Basket { Products = new List<Product> { product, product1 } };
                Basket b2 = new Basket { Products = new List<Product> { product } };
                db.Baskets.AddRange(b1, b2);

                User user1 = new User { Name = "Олег Васильев", Basket = b1, Email = "loha@gmail.com", Password = "qwerty", Role = userRole };

                User user2 = new User { Name = "Олег Васильев2", Basket = b2, Email = "lol@gmail.com", Password = "qwerty", Role = adminRole };
                db.Users.AddRange(user1, user2);
                db.SaveChanges();
            }
        }

        public IActionResult Index()
        {

            IQueryable<Product> products = db.Products.Include(x => x.Category);
            return View(products);
        }
        [Authorize(Roles = "admin, user")]
        public IActionResult asdf()
        {

            IQueryable<User> users = db.Users.Include(x => x.Basket)
                .ThenInclude(z => z.Products).ThenInclude(x => x.Category);
            return View();
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
         *  // Путь к файлу
            string file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files/hello.txt");
            // Тип файла - content-type
            string file_type = "text/plain";
            // Имя файла - необязательно
            string file_name = "hello.txt";
            return PhysicalFile(file_path, file_type, file_name);
        
         */
    }
}