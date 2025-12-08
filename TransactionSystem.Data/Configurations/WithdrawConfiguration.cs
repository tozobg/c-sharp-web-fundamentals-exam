using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionSystem.Models;

namespace TransactionSystem.Data.Configurations
{
    internal class WithdrawConfiguration : IEntityTypeConfiguration<Withdraw>
    {
        public void Configure(EntityTypeBuilder<Withdraw> builder)
        {
            builder.HasOne(w => w.Account)
                   .WithMany(a => a.Withdraws)
                   .HasForeignKey(w => w.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(w => w.Date)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
