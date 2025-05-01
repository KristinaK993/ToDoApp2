
using System.Linq.Expressions;


namespace ToDoApp.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id); //Hämta objekt med ID
        Task<IEnumerable<T>> GetAllAsync(); // hämta alla
        Task< IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate); //hämta med filter
        Task AddAsync (T entity); //lägg till
        Task UpdateAsync (T entity); //updatera
        Task DeleteAsync (T entity); //ta bort 
    }
}
