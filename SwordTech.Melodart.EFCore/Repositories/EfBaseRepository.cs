using Microsoft.EntityFrameworkCore;
using SwordTech.Melodart.EFCore.EFCore;
using SwordTech.Melodart.Helper.Entity;

namespace SwordTech.Melodart.EFCore.Repositories;

public class EfBaseRepository<TEntity> : IEfBaseRepository<TEntity> where TEntity : class, IEntity, new()
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly ServiceDbContext _context;

    public EfBaseRepository(ServiceDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet.Where(x => x.IsDeleted == false).AsQueryable().AsNoTracking();
    }

    public TEntity GetById(Guid id)
    {
        return GetAll().AsNoTracking().First(x => x.IsDeleted == false && x.Id == id);
    }

    public void Add(TEntity entity)
    {
        entity.IsDeleted = false;

        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public void Update(TEntity entity)
    {
        entity.IsDeleted = false;

        _dbSet.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }
}
