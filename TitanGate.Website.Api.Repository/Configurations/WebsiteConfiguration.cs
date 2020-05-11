using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TitanGate.Website.Api.Domain.Entities;

namespace TitanGate.Website.Api.Repository.Configurations
{
    using Website = Domain.Entities.Website;
    public class WebsiteConfiguration : IEntityTypeConfiguration<Website>
    {
        public void Configure(EntityTypeBuilder<Website> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Url).HasMaxLength(200).IsRequired();
            builder.HasOne<Login>(x => x.Login).WithOne(y => y.Website).HasForeignKey<Login>(z => z.LoginOfWebsiteId);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.FilePath).HasMaxLength(150).IsRequired();
        }
    }
}
