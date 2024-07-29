using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuyiDAL.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsArtist { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Artist? Artist { get; set; }
        public ICollection<Playlist>? Playlists { get; set; }
        public ICollection<UserFollowArtist>? UserFollowArtists { get; set; }
        public ICollection<UserLikedSong>? UserLikedSongs { get; set; }
        public ICollection<Notification>? Notifications { get; set; }

    }
}
