using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuyiDAL.Models
{
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; }
        public Guid UserId { get; set; }
        [Required, MaxLength(255)]
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReadAt { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
