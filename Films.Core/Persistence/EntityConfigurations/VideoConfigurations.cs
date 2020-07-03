using Films.Core.Concrete.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Films.Core.Persistence.EntityConfigurations
{
    public class VideoConfigurations : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(v => v.Description).HasMaxLength(500);
            builder.HasOne(v => v.Author)
                .WithMany(a => a.Videos).HasForeignKey(v => v.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(v => v.User)
                .WithMany(u => u.Videos).HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(v => v.AuthorId).IsRequired();
            builder.Property(v => v.LastUpdated).IsRequired();
        }
    }
}