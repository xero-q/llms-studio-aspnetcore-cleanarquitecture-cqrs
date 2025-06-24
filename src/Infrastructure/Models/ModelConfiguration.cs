﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Models;

internal sealed class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.HasKey(m=>m.Id);
        builder.Property(m => m.Id).HasColumnName("id");
        
        builder.Property(m => m.Name).HasColumnName("name").IsRequired().HasMaxLength(255);
        
        builder.Property(m => m.Identifier).HasColumnName("identifier").IsRequired().HasMaxLength(255);
        
        builder.Property(m => m.Temperature).HasColumnName("temperature").IsRequired();
        
        builder.Property(m => m.EnvironmentVariable).HasColumnName("environment_variable");
        
        builder.Property(m => m.ModelTypeId).HasColumnName("model_type_id").IsRequired();
        
        builder.HasIndex(m => m.Identifier).IsUnique();
        
        builder.HasIndex(m => m.EnvironmentVariable).IsUnique();
        
        builder.HasOne(m=>m.Provider)
            .WithMany(mt=>mt.Models)
            .HasForeignKey(m=>m.ModelTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
