using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TitanGate.Website.Api.Domain.Entities;

namespace TitanGate.Website.Api.Repository.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ClientId).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ClientSecret).HasMaxLength(250).IsRequired();
            builder.HasIndex(x => x.ClientId);
        }
    }
}
