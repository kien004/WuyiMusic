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
    public class AlbumConfig : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(al => al.AlbumId);
            builder.Property(al => al.Title).IsRequired().HasMaxLength(100);
            builder.Property(al => al.ReleaseDate).HasColumnType("date");
            builder.Property(al => al.CoverImage).HasMaxLength(255);

            builder.HasOne(al => al.Artist)
                .WithMany(a => a.Albums)
                .HasForeignKey(al => al.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(al => al.Songs)
                .WithOne(s => s.Album)
                .HasForeignKey(s => s.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
