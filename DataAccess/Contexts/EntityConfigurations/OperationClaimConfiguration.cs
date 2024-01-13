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
    public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            builder.ToTable("OperationClaims").HasKey(b => b.Id);

            builder.Property(b => b.Id).HasColumnName("OperationClaimId").IsRequired();
            builder.Property(b => b.Name).HasColumnName("OperationClaimName").IsRequired();

            builder.HasMany(b => b.UserOperationClaims)
             .WithOne(uc => uc.OperationClaim)
                .HasForeignKey(uc => uc.OperationClaimId);


            builder.HasQueryFilter(b => !b.DeletedDate.HasValue);

        }
    }
}
