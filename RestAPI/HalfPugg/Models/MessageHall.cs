using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class MessageHall
    {
        [Key]
        public int ID { get; set; }
        [StringLength(500)] public string Content { get; set; }
        [Required] public DateTime Send_Time { get; set; }        
        [Required] [ForeignKey("PlayerHall")] public int ID_Relation { get; set; }
        public MessageStatus Status { get; set; }
        [JsonIgnore] public virtual PlayerHall PlayerHall { get; set; }
        

    }
}