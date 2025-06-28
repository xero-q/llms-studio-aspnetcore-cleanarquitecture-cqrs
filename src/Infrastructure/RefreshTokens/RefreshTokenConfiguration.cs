using Domain.RefreshTokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.RefreshTokens;

public class RefreshTokenConfiguration:IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Token).HasColumnName("token");
        
        builder.Property(t => t.UserId).HasColumnName("user_id").IsRequired();
        
        builder.HasOne(t=>t.User).WithMany(u => u.RefreshTokens).HasForeignKey(t=>t.UserId).IsRequired();
    }
}
