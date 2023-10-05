﻿using DomainLayer.Entities;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class GameServerServices : IGameServerServices
{
    public readonly IGenericRepository<GameServerEntity> _gameServerRepo;

    public GameServerServices(IGenericRepository<GameServerEntity> gameServerRepo)
    {
        _gameServerRepo = gameServerRepo;
    }
    public async Task<ICollection<GameServerEntity>> List()
    {
        var gameServer = await _gameServerRepo.ListAsync();
        return gameServer;
    }
    public async Task<GameServerEntity> GetById(Guid gameServerId)
    {
        return await _gameServerRepo.FindByIdAsync(gameServerId);
    }
    public async Task<ICollection<GameServerEntity>> GetByGameId(Guid gameId)
    {
        return await _gameServerRepo.WhereAsync(g => g.GameId.Equals(gameId));
    }
    public async Task<int> Count()
    {
        return await _gameServerRepo.CountAsync();
    }
    public async Task Create(GameServerEntity gameServer)
    {

    }
    public async Task Update(Guid gameServerId, GameServerEntity gameServer)
    {

    }
    public async Task Delete(Guid gameServerId) { }
}
