﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PlcTagLib.Common.Models;
using PlcTagLib.Data;

namespace PlcTagLib.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly PlcTagLibDbContext _context;
    private readonly IMapper _mapper;

    public GenericRepository(PlcTagLibDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<T> AddAsync(T entity)
    {
        _ = await _context.AddAsync(entity);
        _ = await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        _ = _context.Set<T>().Remove(entity!);
        _ = await _context.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetAsync(int? id)
    {
        return id == null ? null : await _context.Set<T>().FindAsync(id);
    }

    public async Task<VirtualizeResponse<TResult>> GetAllAsync<TResult>(QueryParameters queryParam)
        where TResult : class
    {
        var totalSize = await _context.Set<T>().CountAsync();
        var items = await _context.Set<T>()
            .Skip(queryParam.StartIndex)
            .Take(queryParam.PageSize)
            .ProjectTo<TResult>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new VirtualizeResponse<TResult> { Items = items, TotalSize = totalSize };
    }

    public async Task UpdateAsync(T entity)
    {
        _ = _context.Update(entity);
        _ = await _context.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }
}
