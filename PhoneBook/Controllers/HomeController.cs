using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using PhoneBook.DB.Data;
using PhoneBook.DB.Infrastructure;
using PhoneBook.DB.Models;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhoneBookData _context;

        public HomeController(IPhoneBookData context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetContactsAsync());
        }
        [HttpGet]
        [Authorize(Roles = $"{RoleNames.User}, {RoleNames.Administrator}")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = $"{RoleNames.User}, {RoleNames.Administrator}")]
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
        [Authorize(Roles = RoleNames.Administrator)]
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
        [Authorize(Roles = RoleNames.Administrator)]
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
        [Authorize(Roles = RoleNames.Administrator)]
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