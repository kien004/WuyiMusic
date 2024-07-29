using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuyiDAL.Models;

namespace WuyiDAL.Configurations
{
    public class PlaylistSongConfig : IEntityTypeConfiguration<PlaylistSong>
    {
        public void Configure(EntityTypeBuilder<PlaylistSong> builder)
        {
            // Primary key
            builder.HasKey(e => e.PlaylistSongId);

            // Relationships
            builder.HasOne(ps => ps.Playlist)
                .WithMany(p => p.PlaylistSongs)
                .HasForeignKey(ps => ps.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ps => ps.Song)
                .WithMany(s => s.PlaylistSongs)
                .HasForeignKey(ps => ps.SongId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
