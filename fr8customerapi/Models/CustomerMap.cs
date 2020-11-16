using fr8model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fr8customerapi.Models
{
    internal class CustomerMap
    {
        public CustomerMap(EntityTypeBuilder<Customer> entityBuilder)
        {
            entityBuilder.HasKey(x => x.CustomerId);
            entityBuilder.ToTable("customer");
            entityBuilder.Property(x => x.CustomerId).HasColumnName("id");
            entityBuilder.Property(x => x.CustomerName).HasColumnName("customername");
            entityBuilder.Property(x => x.CarrierName).HasColumnName("carriername");
            entityBuilder.Property(x => x.FromState).HasColumnName("fromstate");
            entityBuilder.Property(x => x.ToState).HasColumnName("tostate");
            entityBuilder.Property(x => x.LoadWeight).HasColumnName("loadweight");
            entityBuilder.Property(x => x.ChargeAmount).HasColumnName("chargeamount");
        }
    }
}