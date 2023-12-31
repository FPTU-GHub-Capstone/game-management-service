﻿using DomainLayer.Constants;
using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Business;

public class CharacterTypeServices : ICharacterTypeServices
{
    public readonly IGenericRepository<CharacterTypeEntity> _characterTypeRepo;
    public readonly IGenericRepository<GameEntity> _gameRepo;

    public CharacterTypeServices(IGenericRepository<CharacterTypeEntity> characterTypeRepo, IGenericRepository<GameEntity> gameRepo)
    {
        _characterTypeRepo = characterTypeRepo;
        _gameRepo = gameRepo;
    }
    public async Task<ICollection<CharacterTypeEntity>> List()
    {
        return await _characterTypeRepo.ListAsync();
    }
    public async Task<CharacterTypeEntity> GetById(Guid characterTypeId)
    {
        return await _characterTypeRepo.FoundOrThrowAsync(characterTypeId,
            Constants.Entities.CHARACTER_TYPE + Constants.Errors.NOT_EXIST_ERROR);
    }
    public async Task<ICollection<CharacterTypeEntity>> ListCharTypesByGameId(Guid gameId)
    {
        return await _characterTypeRepo.WhereAsync(c => c.GameId.Equals(gameId));
    }
    public async Task Create(CharacterTypeEntity characterType)
    {
        var ctCheck = await _characterTypeRepo.FirstOrDefaultAsync(
            ct => ct.Name.Equals(characterType.Name));
        if (ctCheck != null)
        {
            throw new BadRequestException("Name already exist");
        }
        await _characterTypeRepo.CreateAsync(characterType);
    }
    public async Task Update(CharacterTypeEntity characterType)
    {
        await _characterTypeRepo.UpdateAsync(characterType);
    }
    public async Task Delete(Guid characterTypeId)
    {
        await _characterTypeRepo.DeleteSoftAsync(characterTypeId);
    }
}
