using ExoASP.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExoASP.Models.Congifs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasDefaultValueSql("NEWSEQUENTIALID()").ValueGeneratedOnAdd();

            builder.Property(x => x.Nom).IsRequired().HasMaxLength(30);
            builder.Property(x=>x.Prenom).IsRequired().HasMaxLength(30);
            builder.Property(x=>x.Email).IsRequired().HasMaxLength(40);

        }
    }
}
