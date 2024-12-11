using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TiendaSoftware.DataBase.Entities;

namespace TiendaSoftware.DataBase.Configuration
{
    public class SoftwareTagsConfiguration : IEntityTypeConfiguration<SoftwareTagsEntity>
    {
        public void Configure(EntityTypeBuilder<SoftwareTagsEntity> builder)
        {
            builder.HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedBy)
                .HasPrincipalKey(e => e.Id);



            builder.HasOne(e => e.UpdatedByUser)
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy)
                .HasPrincipalKey(e => e.Id);
               

        }
    }
}
