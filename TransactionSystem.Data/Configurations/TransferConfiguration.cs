using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionSystem.Models;

namespace TransactionSystem.Data.Configurations
{
    internal class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.HasOne(t => t.FromAccount)
                   .WithMany(a => a.TransfersFrom)
                   .HasForeignKey(t => t.FromAccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.ToAccount)
                   .WithMany(a => a.TransfersTo)
                   .HasForeignKey(t => t.ToAccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.Date)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
