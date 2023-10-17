﻿using System.Linq.Expressions;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Contexts;
using DomainLayer.Constants;

namespace RepositoryLayer.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected ApplicationDbContext _context;
    protected DbSet<T> dbSet;
    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        dbSet = context.Set<T>();
    }
    public virtual async Task CreateAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.ModifiedAt = DateTime.UtcNow;
        await dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<T> DeleteAsync(Guid id)
    {
        T _entity = await FindByIdAsync(id);
        if (_entity == null)
        {
            return null;
        }
        dbSet.Remove(_entity);
        await _context.SaveChangesAsync();
        return _entity;
    }

    public virtual async Task DeleteAsync(T _entity)
    {
        dbSet.Remove(_entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        dbSet.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<T> DeleteSoftAsync(Guid id)
    {
        T _entity = await FindByIdAsync(id);
        if (_entity == null)
        {
            return null;
        }
        _entity.DeletedAt = DateTime.UtcNow;
        await UpdateAsync(_entity);
        return _entity;
    }

    public virtual async Task<T> DeleteSoftAsync(T _entity)
    {
        _entity.DeletedAt = DateTime.UtcNow;
        await UpdateAsync(_entity);
        return _entity;
    }

    public virtual async Task<T> FindByIdAsync(Guid id, params string[] navigationProperties)
    {
        var query = ApplyNavigation(navigationProperties);
        T entity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        return entity;
    }

    public virtual async Task<T> FoundOrThrowAsync(Guid id, string message, params string[] navigationProperties)
    {
        var query = ApplyNavigation(navigationProperties);
        T entity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        if (entity is null)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = Constants.ERROR.NOT_FOUND_ERROR;
            }
            throw new NotFoundException(message);
        }
        return entity;
    }

    private IQueryable<T> ApplyNavigation(params string[] navigationProperties)
    {
        var query = dbSet.AsQueryable();
        foreach (string navigationProperty in navigationProperties)
            query = query.Include(navigationProperty);
        return query;
    }

    public virtual async Task<List<T>> ListAsync()
    {
        return await dbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<IList<T>> WhereAsync(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
    {
        List<T> list;
        var query = dbSet.AsQueryable();
        foreach (string navigationProperty in navigationProperties)
            query = query.Include(navigationProperty);//got to reaffect it.

        list = await query.Where(predicate).AsNoTracking().ToListAsync<T>();
        return list;
    }

    public virtual async Task UpdateAsync(T updated)
    {
        //_context.Entry(updated).State = EntityState.Modified;
        updated.ModifiedAt = DateTime.UtcNow;
        _context.Attach(updated).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return dbSet.AsQueryable().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task<long> SumAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, long>> sumExpression)
    {
        return await dbSet.AsQueryable().AsNoTracking().Where(predicate).SumAsync(sumExpression);
    }

    public async Task CreateRangeAsync(IEnumerable<T> entities)
    {
        await _context.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        _context.UpdateRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await dbSet.AsNoTracking().CountAsync();

    }
}
