using ExoASP.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExoASP.Models.Congifs
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatorId).IsRequired().HasColumnType("UNIQUEIDENTIFIER");

            builder.Property(x => x.Nom).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Editeur).IsRequired().HasMaxLength(30);
            builder.Property(x=>x.AnneeDeSortie).IsRequired();
            builder.Property(x=>x.DateAjout).IsRequired();

            builder.HasOne(g=>g.Creator).WithMany(u=>u.AddedGames).HasForeignKey(g => g.CreatorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
