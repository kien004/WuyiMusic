using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuyiDAL.Models
{
    public class UserLikedSong
    {
        public Guid UserLikedSongId { get; set; }
        public Guid UserId { get; set; }
        public Guid SongId { get; set; }
        public DateTime? LikedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("SongId")]
        public Song? Song { get; set; }
    }
}
