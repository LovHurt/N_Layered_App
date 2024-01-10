using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Contexts.EntityConfigurations
{
    public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.ToTable("UserOperationClaim").HasKey(b => b.Id);

            builder.Property(b => b.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(b => b.OperationClaimId).HasColumnName("OperationClaimId").IsRequired();


            builder.HasOne(b => b.User);
            builder.HasOne(b => b.OperationClaim);

            builder.HasQueryFilter(b => !b.DeletedDate.HasValue);

        }
    }
}
