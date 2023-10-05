﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities;

[Table("Level")]
public class LevelEntity : BaseEntity
{
    public string Name { get; set; }
    public int LevelUpPoint { get; set; }
    // 1 Game - M Levels
    public Guid GameId { get; set; }
    public GameEntity Game { get; set; }
    // 1 Level Progress - 1 Level 
    public LevelProgressEntity LevelProgress { get; set; }
}