using Data.Contexts;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public interface IMainRepository<T> where T : class
{
    Task<RepositoryResult<bool>> AddAsync(T entity);
    Task<RepositoryResult<bool>> DeleteAsync(T entity);
    Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<T, bool>> fidnby);
    Task<RepositoryResult<IEnumerable<T>>> GetAllAsync(bool orderByDecending = false, Expression<Func<T, object>>? sortBy = null, Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[]? includes);
    Task<RepositoryResult<IEnumerable<TSelect>>> GetAllAsync<TSelect>(Expression<Func<T, TSelect>> selector, bool orderByDecending = false, Expression<Func<T, object>>? sortBy = null, Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[]? includes);
    Task<RepositoryResult<T>> GetAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[]? includes);
    Task<T?> GetByIdAsync(int id);
    Task<RepositoryResult<bool>> UpdateAsync(T entity);
}

public abstract class MainRepository<T> : IMainRepository<T> where T : class
{


    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;
    public MainRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }


    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }





    public virtual async Task<RepositoryResult<IEnumerable<T>>> GetAllAsync
    (
        bool orderByDecending = false,
        Expression<Func<T, object>>? sortBy = null,
        Expression<Func<T, bool>>? filter = null,
        params Expression<Func<T, object>>[]? includes
    )
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
            query = query.Where(filter);

        if (includes != null && includes.Length != 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (sortBy != null)
        {
            query = orderByDecending ? query.OrderByDescending(sortBy) : query.OrderBy(sortBy);
        }

        var entities = await query.ToListAsync();
        return new RepositoryResult<IEnumerable<T>> { Success = true, StatusCode = 201, Result = entities };
    }

    public virtual async Task<RepositoryResult<IEnumerable<TSelect>>> GetAllAsync<TSelect>
(
    Expression<Func<T, TSelect>> selector,
    bool orderByDecending = false,
    Expression<Func<T, object>>? sortBy = null,
    Expression<Func<T, bool>>? filter = null,
    params Expression<Func<T, object>>[]? includes
)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
            query = query.Where(filter);

        if (includes != null && includes.Length != 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (sortBy != null)
        {
            query = orderByDecending ? query.OrderByDescending(sortBy) : query.OrderBy(sortBy);
        }

        var entities = await query.Select(selector).ToListAsync();
        return new RepositoryResult<IEnumerable<TSelect>> { Success = true, StatusCode = 201, Result = entities };
    }







    public virtual async Task<RepositoryResult<T>> GetAsync
    (Expression<Func<T, bool>> filter,
    params Expression<Func<T, object>>[]? includes)
    {

        IQueryable<T> query = _dbSet;

        if (includes != null && includes.Length != 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        var entity = await query.FirstOrDefaultAsync(filter);
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
        if (entity == null)
            return new RepositoryResult<bool> { Success = false, StatusCode = 400, ErrorMessage = "Entity is null" };

        try
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { Success = true, StatusCode = 201 };
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
