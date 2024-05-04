using Microsoft.EntityFrameworkCore;
using PhoneBook.DB.Infrastructure;
using PhoneBook.DB.Models;

namespace PhoneBook.DB.Data
{
    public class PhoneBookData : IPhoneBookData
    {
        private readonly ApplicationDbContext _context;

        public PhoneBookData(ApplicationDbContext Context)
        {
            _context = Context;
        }

        /// <summary>
        /// Добавить контакт
        /// </summary>
        /// <param name="person">Экземпляр Person</param>
        /// <returns></returns>
        public async Task AddContactAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Обновить контакт
        /// </summary>
        /// <param name="person">Экземпляр Person</param>
        /// <returns></returns>
        public async Task UpdateContactAsync(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Удалить контакт
        /// </summary>
        /// <param name="person">Экземпляр Person</param>
        /// <returns></returns>
        public async Task DeleteContactAsync(Person person)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Получить все контакты
        /// </summary>
        /// <returns>Коллекцию Person</returns>
        public async Task<IEnumerable<Person>> GetContactsAsync()
        {
            return await _context.Persons.ToListAsync();
        }
        /// <summary>
        /// Получить контакт по ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Экземпляр Person</returns>
        public async Task<Person> GetContactByIdAsync(int? id)
        {
            if (id == null || _context.Persons == null)
            {
                return null;
            }

            var person = await _context.Persons
                .FindAsync(id);

            return person;
        }
    }
}