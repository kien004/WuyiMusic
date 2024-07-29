using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WuyiDAL.Models;

namespace WuyiDAL.Configurations
{
    public class SongConfig : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasKey(s => s.SongId); // Khóa chính là SongId

            // Thuộc tính Title là bắt buộc và tối đa 100 ký tự
            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(100);

          

            // Đường dẫn FilePath tối đa 255 ký tự
            builder.Property(s => s.FilePath)
                .HasMaxLength(255);

            // CreatedAt và UpdatedAt là bắt buộc
            builder.Property(s => s.CreatedAt)
                .IsRequired();
            builder.Property(s => s.UpdatedAt)
                .IsRequired();

            // Mối quan hệ với Album
            builder.HasOne(s => s.Album)
                .WithMany(al => al.Songs)
                .HasForeignKey(s => s.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ với Artist
            builder.HasOne(s => s.artist)
            .WithMany(a => a.Songs)
            .HasForeignKey(s => s.ArtistId)
            .OnDelete(DeleteBehavior.Restrict); // Thay đổi này thành Restrict


            // Mối quan hệ một-nhiều với PlaylistSong
            builder.HasMany(s => s.PlaylistSongs)
                .WithOne(ps => ps.Song)
                .HasForeignKey(ps => ps.SongId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ một-nhiều với UserLikedSong
            builder.HasMany(s => s.UserLikedSongs)
                .WithOne(uls => uls.Song)
                .HasForeignKey(uls => uls.SongId)
                .OnDelete(DeleteBehavior.Cascade);

            // Mối quan hệ với Genre
            builder.HasOne(s => s.genre)
                .WithMany(g => g.Songs) 
                .HasForeignKey(s => s.GenreId);
        }
    }
}
