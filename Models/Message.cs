using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MESSAGENOID.Models
{
    public class Message
    {
        [Key]
        [Required]
        public int? message_id { get; set; }

        [Required]
        public string? content { set; get; }

        [Required]
        public DateTime? time_stamp { set; get; }

        [Required]
        [Column("reciever_id")] 
        public string reciever_id { set; get; }
      
        [ForeignKey("reciever_id")]
        public AppUser? Reciever { get; set; }
    }
}
