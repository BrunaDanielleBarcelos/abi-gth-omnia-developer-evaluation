using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SalesConfigurator : IEntityTypeConfiguration<SalesEntity>
{
    public void Configure(EntityTypeBuilder<SalesEntity> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.CodigoVenda)
                    .HasColumnType("CodigoVenda");

        builder.Property(u => u.ProductName)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("ProductName");

        builder.Property(u => u.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)") // Definição explícita do tipo decimal
        .HasColumnType("UnitPrice");

        builder.Property(u => u.Quantity)
            .IsRequired()
            .HasColumnType("Quantity");

        builder.Property(u => u.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
        .HasColumnType("TotalAmount");

        builder.Property(u => u.Discount)
            .HasColumnType("Discount");
    }
}


