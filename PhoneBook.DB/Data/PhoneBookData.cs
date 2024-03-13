using Microsoft.EntityFrameworkCore;
using PhoneBook.DB.Infrastructure;
using PhoneBook.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DB.Data
{
    public class PhoneBookData : IPhoneBookData
    {
        private readonly ApplicationContext context;

        public PhoneBookData(ApplicationContext Context)
        {
            this.context = Context;
        }
        /// <summary>
        /// Добавить контакт
        /// </summary>
        /// <param name="person">Экземпляр Person</param>
        /// <returns></returns>
        public async Task AddContactAsync(Person person)
        {
            context.Persons.Add(person);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Обновить контакт
        /// </summary>
        /// <param name="person">Экземпляр Person</param>
        /// <returns></returns>
        public async Task UpdateContactAsync(Person person)
        {
            context.Persons.Update(person);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Удалить контакт
        /// </summary>
        /// <param name="person">Экземпляр Person</param>
        /// <returns></returns>
        public async Task DeleteContactAsync(Person person)
        {
            context.Persons.Remove(person);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// Получить все контакты
        /// </summary>
        /// <returns>Коллекцию Person</returns>
        public async Task<IEnumerable<Person>> GetContactsAsync()
        {
            return await context.Persons.ToListAsync();
        }
        /// <summary>
        /// Получить контакт по ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Экземпляр Person</returns>
        public async Task<Person> GetContactByIdAsync(int? id)
        {
            if (id == null || context.Persons == null)
            {
                return null;
            }

            var person = await context.Persons
                .FindAsync(id);

            return person;
        }
    }
}
