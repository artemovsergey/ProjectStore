using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectStore.Domen.Models;

namespace ProjectStore.Infrastructure.Configurations;

public class RepositoryConfig : IEntityTypeConfiguration<Repository>
{
    public void Configure(EntityTypeBuilder<Repository> builder)
    {
        builder.Property(x => x.Name).IsRequired();
    }
}