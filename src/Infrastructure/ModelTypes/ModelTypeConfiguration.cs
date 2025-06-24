using Domain.ModelTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelTypes;

internal sealed class ModelTypeConfiguration : IEntityTypeConfiguration<ModelType>
{
    public void Configure(EntityTypeBuilder<ModelType> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name).IsRequired();
        builder.HasIndex(m => m.Name).IsUnique();
    }
}
