using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestCase.Domain;

namespace TestCase.Infrastructure.EntityTypes
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens")
                .HasKey(t => t.Id);

            builder.Property(t => t.Token)
                .IsRequired();

            builder.Ignore(t => t.IsActive);
        }
    }
}
