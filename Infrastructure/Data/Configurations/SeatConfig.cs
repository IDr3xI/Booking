using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class SeatConfig : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasOne(s => s.Room)
            .WithMany(r => r.Seats)
            .HasForeignKey(s => s.RoomId);
        builder.HasMany(s => s.Reservations)
            .WithOne(r => r.Seat)
            .HasForeignKey(r => r.SeatId);
    }
}
