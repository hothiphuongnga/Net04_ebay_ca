using System.ComponentModel;
using AutoMapper;
using ebay.Application.Interfaces;
using ebay.Domain.Interfaces;

namespace ebay.Application.Services;

public class ServiceBase<TEntity, TDto> : IServiceBase<TEntity, TDto>
where TEntity : class
where TDto : class
{
    public readonly IRepositoryBase<TEntity> _repository;
    private readonly IMapper _mapper;
    public ServiceBase(IRepositoryBase<TEntity> repository,
    IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TDto?> GetByIdAsync(int id)
    {
        var res = await _repository.GetByIdAsync(id);
        // res dang laf entity
        // caafn mapveef dto
        var resMap = res !=null ? _mapper.Map<TDto>(res) : null;
        return resMap;
    }

    public Task<TDto> CreateAsync(TDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TDto>> GetAllAsync()
    {
        var response = await _repository.GetAllAsync();

        var resMap = _mapper.Map<List<TDto>>(response);
        return resMap;
        }



    public Task<TDto> UpdateAsync(TDto dto)
    {
        throw new NotImplementedException();
    }
}