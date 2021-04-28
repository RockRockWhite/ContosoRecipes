using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ContosoRecipes.Models
{
    public record Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }

        [Required] public string title { get; init; }
        public string description { get; init; }
        public string[] direstions { get; init; }
        public string[] ingredients { get; init; }
        public string[] tags { get; init; }
        [Required] public string recipe_id { get; init; }
        public DateTime created { get; init; }
        public DateTime updated { get; init; }
    }
}