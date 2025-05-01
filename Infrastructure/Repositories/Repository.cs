using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Infrastructure.Data;
using System.Linq.Expressions;


namespace ToDoApp.Domain.Interfaces
{
   public class Repository<T> : IRepository<T> where T : class //generisk repository som implementerar basfunktion för alla entiteter
    {
        private readonly ToDoDbContext _context; //databascontext
        private readonly DbSet<T> _dbSet; //tabell i databasen för typ T

        public Repository(ToDoDbContext context) //konstruktor som tar in kontext och kopplar tabellen 
        {
            _context = context;
            _dbSet =  _context.Set<T>(); //kopplar automatiskt rätt DbSet baserat på T
        }
        public async Task<T> GetByIdAsync(int id) //Hämta by id
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetAllAsync() //hämta alla
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) //hämtar alla som matchar ett spec villkor(filter)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public async Task AddAsync(T entity) // lägger till ny post i databasen
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity) //uppdaterar post i databasen 
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity) //ta bort 
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
