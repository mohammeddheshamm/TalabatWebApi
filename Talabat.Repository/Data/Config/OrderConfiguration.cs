using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // It Takes componants of it to make it columns in the table of the owner which is Order in this case.
            builder.OwnsOne(O => O.ShippingAddress, NP => NP.WithOwner());

            // Hina ana kont 3aiz a5azen al enum fy al DB string w arga3o integer
            builder.Property(O => O.Status)
                .HasConversion(
                OS => OS.ToString(),
                OS =>(OrderStatus) Enum.Parse(typeof(OrderStatus),OS)
                );

            builder.HasMany(O => O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(18,2)");
        }
    }
}
