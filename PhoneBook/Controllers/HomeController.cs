using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.DB;
using PhoneBook.DB.Infrastructure;
using PhoneBook.DB.Models;
using PhoneBook.Models;
using System.Diagnostics;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhoneBookData _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IPhoneBookData context)
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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetContactsAsync());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                await _context.AddContactAsync(person);
                return Redirect("~/");
            }

            return View(person);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var person = await _context.GetContactByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdateContactAsync(person);
                    return Redirect("~/");
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }

            return View(person);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {

            var person = await _context.GetContactByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var person = await _context.GetContactByIdAsync(id);

            if (person != null)
            {
                await _context.DeleteContactAsync(person);
            }

            return Redirect("~/");
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