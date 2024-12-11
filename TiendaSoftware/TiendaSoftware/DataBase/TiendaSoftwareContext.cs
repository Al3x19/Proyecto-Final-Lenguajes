using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TiendaSoftware.DataBase.Configuration;

namespace TiendaSoftware.DataBase
{
    public class TiendaSoftwareContext : IdentityDbContext<UserEntity>
    {
        private readonly IAuditService _auditService;

        public TiendaSoftwareContext(
            DbContextOptions options,
            IAuditService auditService
            ) : base(options)
        {
            this._auditService = auditService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<TagEntity>()
                .Property(e => e.Name)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.HasDefaultSchema("security");
            modelBuilder.Entity<UserEntity>().ToTable("users");
            modelBuilder.Entity<IdentityRole>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("users_roles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("users_claims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("users_logins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("roles_claims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("users_tokens");

            modelBuilder.ApplyConfiguration(new ListConfiguration());
            modelBuilder.ApplyConfiguration(new ListSoftwareConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new SoftwareConfiguration());
            modelBuilder.ApplyConfiguration(new SoftwareTagsConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new UserDownloadsConfiguration());

            var eTypes = modelBuilder.Model.GetEntityTypes();
            foreach (var type in eTypes)
            {
                var foreignKeys = type.GetForeignKeys();
                foreach (var foreignKey in foreignKeys)
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified
                ));

            foreach (var entry in entries)
            {
                var entity = entry.Entity as BaseEntity;
                if (entity != null)
                {
                    Console.WriteLine(entity.CreatedBy);
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = _auditService.GetUserId();
                        entity.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        entity.UpdatedBy = _auditService.GetUserId();
                        entity.UpdatedDate = DateTime.Now;
                    }
                    Console.WriteLine(entity.CreatedBy);
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<ListEntity> Lists { get; set; }
        public DbSet<SoftwareTagsEntity> SoftwareTags { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<SoftwareEntity> Software { get; set; }
        public DbSet<ListSoftwareEntity> SoftwareLists { get; set; }
        public DbSet<PublisherEntity> Publishers { get; set; }
        public DbSet<UserDownloadsEntity> UserDownloads { get; set; }
    }
}

