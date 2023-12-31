﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("CharacterAttribute")]
public class CharacterAttributeEntity : BaseEntity
{
    public int Value { get; set; }
    //1 Character - M Character Attribute
    public Guid CharacterId { get; set; }
    public CharacterEntity Character { get; set; }
    //1 Attribute Group - M Character Attribute
    public Guid AttributeGroupId { get; set; }
    public AttributeGroupEntity AttributeGroup { get; set; }
}
