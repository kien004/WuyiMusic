using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuyiDAL.Models
{
    public class Song
    {
        [Key]
        public Guid SongId { get; set; }
        
        public Guid ArtistId { get; set; }
        public Guid? AlbumId { get; set; }       
        public Guid? GenreId { get; set; }
        
        public string? Title { get; set; }
        public int? Duration { get; set; }
        public string? FilePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public  Album? Album { get; set; }
        public  Artist? artist {  get; set; }
        public  ICollection<PlaylistSong>? PlaylistSongs { get; set; }
        public  ICollection<UserLikedSong>? UserLikedSongs { get; set; }
        public  Genre? genre { get; set; }
    }
}
