﻿using Dotnet.AspNetCore.Samples.WebApi.Data;
using Dotnet.AspNetCore.Samples.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Dotnet.AspNetCore.Samples.WebApi.Services;

public class PlayerService : IPlayerService
{
    private const string MemoryCacheKey_Retrieve = "MemoryCacheKey_Retrieve";
    private readonly PlayerContext _playerContext;
    private readonly ILogger<PlayerService> _logger;
    private readonly IMemoryCache _memoryCache;

    public PlayerService(PlayerContext playerContext, ILogger<PlayerService> logger, IMemoryCache memoryCache)
    {
        _playerContext = playerContext;
        _logger = logger;
        _memoryCache = memoryCache;
    }

    /*
    -----------------------------------------------------------------------------------------------
    Create
    -----------------------------------------------------------------------------------------------
    */
    public async Task CreateAsync(Player player)
    {
        _playerContext.Entry(player).State = EntityState.Added;
        await _playerContext.SaveChangesAsync();
        _memoryCache.Remove(MemoryCacheKey_Retrieve);
    }

    /*
    -----------------------------------------------------------------------------------------------
    Retrieve
    -----------------------------------------------------------------------------------------------
    */
    public async Task<List<Player>> RetrieveAsync()
    {
        if (_memoryCache.TryGetValue(MemoryCacheKey_Retrieve, out List<Player>? players)
            && players != null)
        {
            _logger.Log(LogLevel.Information, "Players retrieved from MemoryCache.");
            return players;
        }
        else
        {
            // Introduced on purpose to simulate a real database query with a
            // delay of beteen 1 and 2 seconds.
            await Task.Delay(new Random().Next(1000, 2000));

            players = await _playerContext.Players.ToListAsync();
            _memoryCache.Set(MemoryCacheKey_Retrieve, players, GetMemoryCacheEntryOptions());

            _logger.Log(LogLevel.Information, "Players retrieved from DbContext.");
            return players;
        }
    }

    public ValueTask<Player?> RetrieveById(long id)
    {
        return _playerContext.Players.FindAsync(id);
    }

    /*
    -----------------------------------------------------------------------------------------------
    Update
    -----------------------------------------------------------------------------------------------
    */
    public async Task UpdateAsync(Player player)
    {
        //_playerContext.Entry(player).State = EntityState.Modified;

        try
        {
            if (await _playerContext.Players.FindAsync(player.Id) is Player entity)
            {
                entity.MapFrom(player);
                await _playerContext.SaveChangesAsync();
                _memoryCache.Remove(MemoryCacheKey_Retrieve);
            }
                
        }
        catch (DbUpdateConcurrencyException exception)
        {
            // https://learn.microsoft.com/en-us/ef/core/saving/concurrency
            foreach (var entry in exception.Entries)
            {
                if (entry.Entity is Player)
                {
                    throw new NotImplementedException(
                        "Concurrency conflicts handling not implemented for "
                        + entry.Metadata.Name);
                }
            }
        }
    }

    /*
    -----------------------------------------------------------------------------------------------
    Delete
    -----------------------------------------------------------------------------------------------
    */
    public async Task DeleteAsync(long id)
    {
        var player = await _playerContext.Players.FindAsync(id);

        if (player != null)
        {
            _playerContext.Entry(player).State = EntityState.Deleted;
            await _playerContext.SaveChangesAsync();
            _memoryCache.Remove(MemoryCacheKey_Retrieve);
        }
    }

    private MemoryCacheEntryOptions GetMemoryCacheEntryOptions()
    {
        return new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromHours(1))
            .SetSlidingExpiration(TimeSpan.FromMinutes(10))
            .SetPriority(CacheItemPriority.Normal);
    }
}
