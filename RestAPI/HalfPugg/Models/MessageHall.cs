using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class MessageHall
    {
        [Key]
        public int ID { get; set; }
        [StringLength(400)]
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Send_Time { get; set; }
        [Required]
        public DateTime View_Time { get; set; }
        [Required]
        public Gamer ID_User { get; set; }
        [Required]
        public Hall ID_Recipient { get; set; }
        [Required]
        public char Status { get; set; }

        [Required] public DateTime CreateAt { get; set; }
        [Required] public DateTime AlteredAt { get; set; }
    }
}