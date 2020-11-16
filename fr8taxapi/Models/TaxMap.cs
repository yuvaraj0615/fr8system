using fr8model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fr8taxapi.Models
{
    internal class TaxMap
    {

        public TaxMap(EntityTypeBuilder<Tax> entityBuilder)
        {
            entityBuilder.HasKey(x => x.StateId);
            entityBuilder.ToTable("tax");
            entityBuilder.Property(x => x.StateId).HasColumnName("id");
            entityBuilder.Property(x => x.StateName).HasColumnName("statename");
            entityBuilder.Property(x => x.TaxPercent).HasColumnName("taxpercent");
        }
    }
}