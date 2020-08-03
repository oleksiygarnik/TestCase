using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestCase.Domain;

namespace TestCase.Infrastructure.EntityTypes
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions").HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("transaction_id")
                .ValueGeneratedNever();

            builder.Property(t => t.ClientName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
