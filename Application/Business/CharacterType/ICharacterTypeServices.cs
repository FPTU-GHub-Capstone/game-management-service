﻿using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface ICharacterTypeServices
{
    Task<ICollection<CharacterTypeEntity>> List();
    Task<CharacterTypeEntity> GetById(Guid characterTypeId);
    Task<ICollection<CharacterTypeEntity>> GetByGameId(Guid gameId);
    Task<int> Count();
    Task Create(CharacterTypeEntity characterType);
    Task Update(Guid characterTypeId, CharacterTypeEntity characterType);
    Task Delete(Guid characterTypeId);
}