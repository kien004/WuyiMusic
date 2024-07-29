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
    public class ArtistConfig : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasKey(a => a.ArtistId);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Bio).HasColumnType("text");
            builder.Property(a => a.Avatar).HasMaxLength(250);
            builder.Property(a => a.CreatedAt).IsRequired();
            builder.Property(a => a.UpdatedAt).IsRequired();

            // Relationships
            builder.HasMany(a => a.Albums)
                .WithOne(al => al.Artist)
                .HasForeignKey(al => al.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.UserFollowArtists)
                .WithOne(ufa => ufa.Artist)
                .HasForeignKey(ufa => ufa.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
