using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    public class RegiaoMap : IEntityTypeConfiguration<Regiao>
    {        
        public void Configure(EntityTypeBuilder<Regiao> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Nome).HasMaxLength(1024).IsRequired();
            builder.Property(r => r.IsActive).IsRequired();

            builder.HasMany(r => r.RegiaoCidades)
                    .WithOne(rc => rc.Regiao)
                    .HasForeignKey(rc => rc.RegiaoId);
        }
    }
}
