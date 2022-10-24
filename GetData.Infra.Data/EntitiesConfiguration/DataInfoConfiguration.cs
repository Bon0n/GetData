using GetData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetData.Infra.Data.EntitiesConfiguration
{
    public class DataInfoConfiguration : IEntityTypeConfiguration<DataInfo>
    {
        public void Configure(EntityTypeBuilder<DataInfo> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.LocalIpv4).HasMaxLength(30).IsRequired();
            builder.Property(i => i.PublicIpv4).HasMaxLength(50).IsRequired();
            builder.Property(h => h.Hostname).HasMaxLength(30).IsRequired();
            builder.Property(m => m.MacAddr).HasMaxLength(50).IsRequired();
        }
    }
}