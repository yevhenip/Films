using Films.Core.Concrete.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Films.Core.Persistence.EntityConfigurations
{
    public class VideoGenreConfigurations : IEntityTypeConfiguration<VideoGenre>
    {
        public void Configure(EntityTypeBuilder<VideoGenre> builder)
        {
            builder.HasKey(vg => new {vg.VideoId, vg.GenreId});
            builder.HasOne(vg => vg.Video)
                .WithMany(v => v.Genres)
                .HasForeignKey(vg => vg.VideoId);
            builder.HasOne(vg => vg.Genre)
                .WithMany(v => v.Videos)
                .HasForeignKey(vg => vg.GenreId);
        }
    }
}