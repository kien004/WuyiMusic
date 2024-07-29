using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuyiDAL.Models
{
    public class Artist
    {
        [Key]
        public Guid ArtistId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Avatar { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public User? User { get; set; }
        public ICollection<Song>? Songs { get; set; }
        public ICollection<Album>? Albums { get; set; }
        public ICollection<UserFollowArtist>? UserFollowArtists { get; set; }
    }
}
