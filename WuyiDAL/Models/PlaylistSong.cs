using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuyiDAL.Models
{
    public class PlaylistSong
    {
        public Guid PlaylistSongId { get; set; }
        public Guid PlaylistId { get; set; }
        public Guid SongId { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("PlaylistId")]
        public Playlist? Playlist { get; set; }
        [ForeignKey("SongId")]
        public Song? Song { get; set; }
    }
}
