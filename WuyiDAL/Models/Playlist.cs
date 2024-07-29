using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuyiDAL.Models
{
    public class Playlist
    {
        [Key]
        public Guid PlaylistId { get; set; }
        public Guid UserId { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public User? User { get; set; }
        public ICollection<PlaylistSong>? PlaylistSongs { get; set; }
    }
}
