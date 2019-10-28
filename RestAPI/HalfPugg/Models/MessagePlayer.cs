using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HalfPugg.Models
{
    public class MessagePlayer
    {
        [Key] public int ID_Message { get; set; }
        [StringLength(400)] [Required] public string Content { get; set; }
        [Required] [ForeignKey("User")] public int ID_User { get; set; }
        [Required] [ForeignKey("Recipient")] public int ID_Recipient { get; set; }
        [Required] [StringLength(1)] public string Status { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        [JsonIgnore] public virtual Player User { get; set; }
        [JsonIgnore] public virtual Player Recipient { get; set; }

    }
}