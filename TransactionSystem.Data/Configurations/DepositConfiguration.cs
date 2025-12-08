using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionSystem.Models;

namespace TransactionSystem.Data.Configurations
{
    internal class DepositConfiguration : IEntityTypeConfiguration<Deposit>
    {
        public void Configure(EntityTypeBuilder<Deposit> builder)
        {
            builder.HasOne(d => d.Account)
                   .WithMany(a => a.Deposits)
                   .HasForeignKey(d => d.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(d => d.Date)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
