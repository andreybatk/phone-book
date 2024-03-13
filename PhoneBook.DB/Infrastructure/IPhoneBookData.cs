using PhoneBook.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DB.Infrastructure
{
    public interface IPhoneBookData
    {
        Task<IEnumerable<Person>> GetContactsAsync();
        Task<Person> GetContactByIdAsync(int? id);
        Task AddContactAsync(Person person);
        Task UpdateContactAsync(Person person);
        Task DeleteContactAsync(Person person);
    }
}