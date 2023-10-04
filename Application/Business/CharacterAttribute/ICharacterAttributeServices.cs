﻿using DomainLayer.Entities;

namespace ServiceLayer.Business.CharacterAttribute;

public interface ICharacterAttributeServices
{
    Task<ICollection<CharacterAttributeEntity>> List();
    Task<CharacterAttributeEntity> GetById(Guid characterAttributeid);
    Task<ICollection<CharacterAttributeEntity>> GetByCharacterId(Guid id);
    Task<ICollection<CharacterAttributeEntity>> GetByAttributeGroupId(Guid id);
    Task<int> Count();
    Task Create(CharacterAttributeEntity characterAttribute);
    Task Update(Guid characterAttributeid, CharacterAttributeEntity characterAttribute);
    Task Delete(Guid characterAttributeid);
}
