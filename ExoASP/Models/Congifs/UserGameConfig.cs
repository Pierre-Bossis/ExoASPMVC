using ExoASP.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExoASP.Models.Congifs
{
    public class UserGameConfig : IEntityTypeConfiguration<UserGame>
    {
        public void Configure(EntityTypeBuilder<UserGame> builder)
        {
            builder.HasKey(ug => new { ug.UserId, ug.GameId });

            builder.Property(ug => ug.DateAchat).IsRequired();
            builder.Property(ug => ug.EstOccasion).IsRequired();
            //date achat en getdate ici et pas dans gamerepository

            builder.HasOne(ug => ug.Game)
                .WithMany(g => g.JoinUsers)
                .HasForeignKey(ug => ug.GameId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ug => ug.User)
                .WithMany(u => u.JoinGames)
                .HasForeignKey(ug => ug.UserId)
                 .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
