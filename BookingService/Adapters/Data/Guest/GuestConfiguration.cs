using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Guest
{
    public class GuestConfiguration : IEntityTypeConfiguration<Domain.Guest.Entity.Guest>
    {
        public void Configure(EntityTypeBuilder<Domain.Guest.Entity.Guest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.DocumentId)
                .Property(x => x.IdNumber);

            builder.OwnsOne(x => x.DocumentId)
                .Property(x => x.DocumentType);
        }
    }
}
