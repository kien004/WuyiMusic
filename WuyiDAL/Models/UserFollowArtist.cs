using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuyiDAL.Models
{
    public class UserFollowArtist
    {
        public Guid UserFollowArtistId { get; set; }
        public Guid UserId { get; set; }
        public Guid ArtistId { get; set; }
        public DateTime? FollowedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public User? User { get; set; }
        [ForeignKey("ArtistId")]
        public Artist? Artist { get; set; }
    }
}
