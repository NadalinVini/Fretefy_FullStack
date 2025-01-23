using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    public class RegiaoCidadeMap : IEntityTypeConfiguration<RegiaoCidade>
    {
        public void Configure(EntityTypeBuilder<RegiaoCidade> builder)
        {
            builder.HasKey(rc => rc.Id); 

            builder.HasOne(rc => rc.Regiao)
                   .WithMany(r => r.RegiaoCidades) 
                   .HasForeignKey(rc => rc.RegiaoId); 

            builder.HasOne(rc => rc.Cidade)
                   .WithMany()  
                   .HasForeignKey(rc => rc.CidadeId); 
        }
    }

}
