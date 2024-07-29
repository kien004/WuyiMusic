using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WuyiDAL.Models;

namespace WuyiDAL.Configurations
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.CreatedAt).IsRequired();
            builder.Property(u => u.UpdatedAt).IsRequired();

            // Relationships
            builder.HasMany(u => u.Playlists)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            /// nếu sai thì chỉ có ở đây
            builder.HasOne(u => u.Artist)
                .WithOne(a => a.User)
                .HasForeignKey<Artist>(a => a.ArtistId)
                .IsRequired(false) // Cho phép ArtistId có thể null (tức là không có nghệ sĩ tương ứng)
                .HasConstraintName("FK_User_Artist")
                .OnDelete(DeleteBehavior.Restrict);

            ///
            builder.HasMany(u => u.UserFollowArtists)
                .WithOne(ufa => ufa.User)
                .HasForeignKey(ufa => ufa.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.UserLikedSongs)
                .WithOne(uls => uls.User)
                .HasForeignKey(uls => uls.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
