using PhoneBook.DB.Models;

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