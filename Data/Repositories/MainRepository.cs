using Data.Contexts;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class MainRepository<T> where T : class
{


        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public MainRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public virtual async Task<RepositoryResult<IEnumerable<T>>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
        return new RepositoryResult<IEnumerable<T>> { Success = true, StatusCode = 201, Result = entities };
    }
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

    public virtual async Task<RepositoryResult<T>> GetAsync(Expression<Func<T, bool>> fidnby)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(fidnby);
        return entity == null
            ? new RepositoryResult<T> { Success = false, StatusCode = 404, ErrorMessage = "Entity not found" }
            : new RepositoryResult<T> { Success = true, StatusCode = 200, Result = entity };
    }

    public virtual async Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<T, bool>> fidnby)
    {
        var exists = await _dbSet.AnyAsync(fidnby);
        return !exists
            ? new RepositoryResult<bool> { Success = false, StatusCode = 404, ErrorMessage = "Entity not found" }
            : new RepositoryResult<bool> { Success = true, StatusCode = 200 };
    }

    public virtual async Task<RepositoryResult<bool>> AddAsync(T entity)
        {
            if(entity == null)
            return new RepositoryResult<bool> { Success = false, StatusCode = 400, ErrorMessage = "Entity is null" };

        try {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Success = true, StatusCode = 201};
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error adding entity: {ex.Message}");
            return new RepositoryResult<bool> { Success = false, StatusCode = 500, ErrorMessage = ex.Message };
        }
    }

    public virtual async Task<RepositoryResult<bool>> UpdateAsync(T entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { Success = false, StatusCode = 400, ErrorMessage = "Entity is null" };

        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Success = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error adding entity: {ex.Message}");
           return new RepositoryResult<bool> { Success = false, StatusCode = 500, ErrorMessage = "Entity not found" };
        }
    }
    public virtual async Task<RepositoryResult<bool>> DeleteAsync(T entity)
    {
        if (entity == null)
            return new RepositoryResult<bool> { Success = false, StatusCode = 400, ErrorMessage = "Entity is null" };

        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Success = true, StatusCode = 200 };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error adding entity: {ex.Message}");
           return new RepositoryResult<bool> { Success = false, StatusCode = 500, ErrorMessage = "Entity not found" };
        }
    }
}
