using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class MessageHall
    {
        [Key] public int ID { get; set; }
        [StringLength(400)] [Required] public string Content { get; set; }
        [Required] public DateTime Send_Time { get; set; }
        [Required] public DateTime View_Time { get; set; }
        [Required] [ForeignKey("User")] public int ID_User { get; set; }
        [Required] [ForeignKey("Recipient")] public int ID_Recipient { get; set; }
        [Required] public char Status { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        public virtual Player User { get; set; }
        public virtual Hall Recipient { get; set; }
    }
}