﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Room
{
    public class RoomConfiguration : IEntityTypeConfiguration<Domain.Room.Entity.Room>
    {
        public void Configure(EntityTypeBuilder<Domain.Room.Entity.Room> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Price)
                .Property(x => x.Currency);

            builder.OwnsOne(x => x.Price)
                .Property(x => x.Value);
        }
    }
}
