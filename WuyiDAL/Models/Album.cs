using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuyiDAL.Models
{
    public class Album
    {
        [Key]
        public Guid AlbumId { get; set; }
        public Guid ArtistId { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? CoverImage { get; set; }
        [ForeignKey("ArtistId")]
        public Artist? Artist { get; set; }
        public ICollection<Song>? Songs { get; set; }
        
    }
}
