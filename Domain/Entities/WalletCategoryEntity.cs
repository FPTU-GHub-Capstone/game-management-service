﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("WalletCategory")]
public class WalletCategoryEntity : BaseEntity
{
    public string Name { get; set; }
    //1 Game - M Wallet Category
    public Guid GameId { get; set; }
    public GameEntity Game { get; set; }
    // 1 Wallet Category - M Wallet
    public virtual ICollection<WalletEntity>? WalletEntities { get; set; }
}