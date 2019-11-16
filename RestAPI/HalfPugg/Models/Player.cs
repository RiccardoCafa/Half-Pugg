using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HalfPugg.Hubs;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class Player
    {
        [Key] public int ID { get; set; }       
        [Required] [StringLength(30)] public string Name { get; set; }
        [Required] [StringLength(70)] public string LastName { get; set; }
        [Required] [StringLength(50)] [Index(IsUnique = true)] public string Nickname { get; set; }
        [Required] [StringLength(100)] public string HashPassword { get; set; } 
        [StringLength(300)] public string Bio { get; set; }
        [Required] [EmailAddress] public string  Email { get; set; }
        [Required] public DateTime Birthday { get; set; }
        [StringLength(500)] public string ImagePath { get; set; }
        [Required] [StringLength(1)] public string Type { get; set; }
        public string? ID_Branch { get; set; }
        public string Slogan { get; set; }
        [Required] [StringLength(1)] public string Sex { get; set; }
        [StringLength(100)] public string Genre { get; set; }
        [JsonIgnore] public ICollection<MessageHall> MessageHalls { get; set; }
        [JsonIgnore] public ICollection<MessageGroup> MessageGroups { get; set; }
        //Chat

        [JsonIgnore] public ICollection<ChatConnection> ChatConnections { get; set; }
        [JsonIgnore] public ICollection<PlayerGroup> Groups { get; set; }
        [JsonIgnore] public ICollection<PlayerHall> Halls { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
    }
}