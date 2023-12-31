﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainLayer.Entities;

[Table("User")]
public class UserEntity : BaseEntity
{
    public string Username { get; set; }
    public string Uid { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Avatar { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Code { get; set; } = "";
    public int Status { get; set; } = 0;
    public float Balance { get; set; } = 0;
    [JsonIgnore]
    // 1 User - M Characters
    public virtual ICollection<CharacterEntity>? Characters { get; set; }
    [JsonIgnore]
    //1 User - M Payment
    public virtual ICollection<PaymentEntity>? Payments { get; set; }

}
