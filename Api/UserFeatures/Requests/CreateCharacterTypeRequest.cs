﻿using AutoWrapper.Filters;
using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests
{
    public class CreateCharacterTypeRequest : IMapTo<CharacterTypeEntity>, IMapFrom<CharacterTypeEntity>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public JsonObject BaseProperties { get; set; } //JSON
    }
}
