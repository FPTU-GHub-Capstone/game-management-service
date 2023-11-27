﻿using DomainLayer.Entities;

namespace ServiceLayer.Business;

public interface IAttributeGroupServices
{
    Task<ICollection<AttributeGroupEntity>> List();
    Task<AttributeGroupEntity> GetById(Guid attributeGroupid);
    Task Create(AttributeGroupEntity attributeGroup);
    Task Update(AttributeGroupEntity attributeGroup);
    Task Delete(Guid attributeGroupid);
}
