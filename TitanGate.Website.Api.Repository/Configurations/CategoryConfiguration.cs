using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TitanGate.Website.Api.Domain.Entities;

namespace TitanGate.Website.Api.Repository.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);
            SeedCategories(builder);
        }

        private static void SeedCategories(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                            new Category { Id = 1, Name = "eCommerce" },
                            new Category { Id = 2, Name = "Business" },
                            new Category { Id = 3, Name = "Entertainment" },
                            new Category { Id = 4, Name = "Portfolio" },
                            new Category { Id = 5, Name = "Media" },
                            new Category { Id = 6, Name = "Brochure" },
                            new Category { Id = 7, Name = "Nonprofit" },
                            new Category { Id = 8, Name = "Educational" },
                            new Category { Id = 9, Name = "Infopreneur" },
                            new Category { Id = 10, Name = "Community Forum" },
                            new Category { Id = 11, Name = "Personal" }
                        );
        }
    }
}
