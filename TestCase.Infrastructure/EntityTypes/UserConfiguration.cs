using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestCase.Domain;

namespace TestCase.Infrastructure.EntityTypes
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(t => t.Id);

            builder.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Password)
                .IsRequired();
        }
    }
}
