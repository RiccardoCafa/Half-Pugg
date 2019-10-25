using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfPugg.Models
{
    public class MessagePlayer
    {
        [Key] public int ID_Message { get; set; }
        [StringLength(400)] [Required] public string Content { get; set; }
        [Required] public int ID_User { get; set; }
        [Required] public int ID_Recipient { get; set; }
        [Required] public char Status { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime AlteredAt { get; set; }
        //[ForeignKey("ID_User")] public virtual Player User { get; set; }
        //[ForeignKey("ID_Recipient")] public virtual Player Recipient { get; set; }

    }
}