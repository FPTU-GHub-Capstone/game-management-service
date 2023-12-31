﻿using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;
using System.Security.Cryptography;

namespace ServiceLayer.Business;

public class LevelServices : ILevelServices
{
    public readonly IGenericRepository<LevelEntity> _levelRepo;
    public LevelServices(IGenericRepository<LevelEntity> levelRepo)
    {
        _levelRepo = levelRepo;
    }
    public async Task<ICollection<LevelEntity>> List()
    {
        return await _levelRepo.ListAsync();
    }
    public async Task<LevelEntity> GetById(Guid levelId)
    {
        return await _levelRepo.FoundOrThrowAsync(levelId,
            Constants.Entities.LEVEL + Constants.Errors.NOT_EXIST_ERROR);
    }
    public async Task<ICollection<LevelEntity>> List(Guid[] levelIds)
    {
        var levels = await _levelRepo.WhereAsync(level => levelIds.Contains(level.Id));
        return levels;
    }
    public async Task<ICollection<LevelEntity>> ListLevelsByGameId(Guid gameId)
    {
        var levelsList =  await _levelRepo.WhereAsync(l => l.GameId.Equals(gameId));
        return levelsList.OrderBy(l => l.LevelNo).ToList();
    }
    public async Task Create(List<LevelEntity> level)
    {
        await _levelRepo.CreateRangeAsync(level);
    }
    public async Task Update(LevelEntity level)
    {
        await _levelRepo.UpdateAsync(level);
    }
    public async Task Delete(LevelEntity level)
    {
        var ListLevelsByGameId = await _levelRepo.WhereAsync(l => l.GameId == level.GameId && l.LevelNo > level.LevelNo);
        foreach(var singleLevel in ListLevelsByGameId)
        {
            singleLevel.LevelNo -= 1;
            await _levelRepo.UpdateAsync(singleLevel);
        }
        await _levelRepo.DeleteSoftAsync(level.Id);
    }
}