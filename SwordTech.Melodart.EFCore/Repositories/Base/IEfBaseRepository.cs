using Microsoft.EntityFrameworkCore.Storage;
using SwordTech.Melodart.Helper.Entity;

namespace SwordTech.Melodart.EFCore.Repositories.Base;

public interface IEfBaseRepository<TEntity> where TEntity : IEntity, new()
{
    public IQueryable<TEntity> GetAll();
    public TEntity GetById(Guid id);
    public void Add(TEntity entity);
    public void Update(TEntity entity);
    public void Delete(TEntity entity);

    public IDbContextTransaction BeginTransaction();
}
