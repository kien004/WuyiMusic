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
    public class UserFollowArtistConfig:IEntityTypeConfiguration<UserFollowArtist>
    {
        public void Configure(EntityTypeBuilder<UserFollowArtist> builder)
        {
            
            builder.HasKey(e => e.UserFollowArtistId);

            builder.HasOne(ufa => ufa.User)
                .WithMany(u => u.UserFollowArtists)
                .HasForeignKey(ufa => ufa.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ufa => ufa.Artist)
                .WithMany(a => a.UserFollowArtists)
                .HasForeignKey(ufa => ufa.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
