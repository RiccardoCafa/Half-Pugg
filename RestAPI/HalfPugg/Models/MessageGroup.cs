using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class MessageGroup
    {
        [Key]
        public int ID { get; set; }
        [StringLength(500)] public string Content { get; set; }       
        [Required] public DateTime Send_Time { get; set; }           
        [Required] [ForeignKey("PlayerGroup")] public int ID_Relation { get; set; }
        [Required] [ForeignKey("Group")] public int? IdGroup { get; set; }
        public MessageStatus Status { get; set; }
        [JsonIgnore] public virtual PlayerGroup PlayerGroup { get; set; }
        [JsonIgnore] public virtual Group? Group { get; set; }


    }
}