using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal class ReservationConfig : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.BookDate)
            .IsRequired();
        builder.HasOne(r => r.Seat)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.SeatId);
    }
}
