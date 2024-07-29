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
    public class UserLikedSongConfig : IEntityTypeConfiguration<UserLikedSong>
    {
        public void Configure(EntityTypeBuilder<UserLikedSong> builder)
        {
            
            builder.HasKey(e => e.UserLikedSongId);

            builder.HasOne(uls => uls.User)
                .WithMany(u => u.UserLikedSongs)
                .HasForeignKey(uls => uls.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(uls => uls.Song)
                .WithMany(s => s.UserLikedSongs)
                .HasForeignKey(uls => uls.SongId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
