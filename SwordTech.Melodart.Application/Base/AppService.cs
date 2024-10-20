using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.EFCore.Repositories;
using SwordTech.Melodart.Helper.Entity;
using Microsoft.Extensions.Hosting;
using SwordTech.Melodart.EFCore.Repositories.Base;

namespace SwordTech.Melodart.Application.Base;

public class AppService<TEntity, TListDto, TDetailDto> : IAppService<TEntity, TListDto, TDetailDto>
    where TEntity : IEntity, new()
{
    protected readonly IEfBaseRepository<TEntity> _repository;
    protected readonly IMapper _mapper;

    public AppService(IEfBaseRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task<IList<TListDto>> GetAll()
    {
        return await _mapper.ProjectTo<TListDto>(_repository.GetAll()).ToListAsync();
    }

    public virtual async Task<IList<TListDto>> GetAll(Expression<Func<TListDto, bool>> predicate)
    {
        return await _mapper.ProjectTo<TListDto>(_repository.GetAll()).Where(predicate).ToListAsync();
    }

    public virtual async Task<TDetailDto> GetById(Guid id)
    {
        return await _mapper.ProjectTo<TDetailDto>(_repository.GetAll().Where(x => x.Id == id)).FirstOrDefaultAsync();
    }

    public async Task<string> SaveImage(IWebHostEnvironment env, IFormFile file)
    {
        // var pathToSave = Path.Combine(env.ContentRootPath, "wwwroot/images");
        var pathToSave = Path.Combine(env.WebRootPath, "images");

        var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName); 

        var fullPath = Path.Combine(pathToSave, fileName);

        using (var stream = System.IO.File.Create(fullPath))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }
}


public class AppService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> : AppService<TEntity, TListDto, TDetailDto>,
    IAppService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    where TEntity : IEntity, new()
{
    public AppService(IEfBaseRepository<TEntity> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public virtual async Task<TDetailDto> Create(TCreateDto input)
    {
        var entity = _mapper.Map<TEntity>(input);
        _repository.Add(entity);

        return await GetById(entity.Id);
    }

    public virtual async Task<TDetailDto> Update(Guid id, TUpdateDto input)
    {
        var entity = _repository.GetById(id);
        _mapper.Map(input,entity);
        _repository.Update(entity);

        return await GetById(entity.Id);
    }

    public virtual async Task Delete(Guid id)
    {
        var entity = _repository.GetById(id);

        if (entity != null)
        {
            _repository.Delete(entity);
        }
    }
}
