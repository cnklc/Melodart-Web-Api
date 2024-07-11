using System.Linq.Expressions;
using SwordTech.Melodart.Helper.Entity;

namespace SwordTech.Melodart.Application.Contract.Base;

public interface IAppService<TEntity, TListDto, TDetailDto> where TEntity : IEntity, new()
{
    Task<IList<TListDto>> GetAll();
    Task<IList<TListDto>> GetAll(Expression<Func<TListDto, bool>> predicate);
    Task<TDetailDto> GetById(Guid id);
}


public interface IAppService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> : IAppService<TEntity, TListDto, TDetailDto>
    where TEntity : IEntity, new()
{
    Task<TDetailDto> Create(TCreateDto input);
    Task<TDetailDto> Update(Guid id, TUpdateDto input);
    Task Delete(Guid id);
}
