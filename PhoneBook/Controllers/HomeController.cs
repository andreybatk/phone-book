using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.DB;
using PhoneBook.DB.Models;
using PhoneBook.Models;
using System.Diagnostics;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;

            //for (int i = 0; i < 10; i++)
            //{
            //    var person = new Person();
            //    person.FirstName = $"Имя {i}";
            //    person.SecondName = $"Фамилия {i}";
            //    person.LastName = $"Отчество {i}";
            //    person.Address = $"Москва, ул. Гагарина д. {i}";
            //    person.PhoneNumber = $"+7133700000{i}";
            //    person.Description = $"-";
            //    _context.Persons.Add(person);
            //}
            //_context.SaveChanges();
        }

        public async Task<IActionResult> Index()
        {
            return _context.Persons != null ?
                        View(await _context.Persons.ToListAsync()) :
                        Problem("Entity set 'ApplicationContext.Persons'  is null.");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var project = await _context.Persons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}