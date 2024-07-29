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
    internal class NotificationConfig : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.NotificationId);
            builder.Property(n => n.Title).IsRequired().HasMaxLength(255);
            builder.Property(n => n.Message).HasColumnType("text");
            builder.Property(n => n.CreatedAt).IsRequired();
            builder.Property(n => n.ReadAt).HasColumnType("datetime2");

            builder.HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
