using Microsoft.EntityFrameworkCore;
using WpfForM_CRM.Entities;

namespace WpfForM_CRM.Context
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users => Set<User>();
        public DbSet<Shop> Shops => Set<Shop>();


        //public AppDbContext(DbContextOptions<AppDbContext>  options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=mcrmshopdb;user=root;",
                ServerVersion.Parse("8.0.24-mysql"));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50);

                entity.HasIndex(e => e.UserName)
                    .IsUnique();
            });


            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(50);

                entity.HasIndex(sh => sh.Name)
                     .IsUnique();
            });


            //modelBuilder.Entity<Category>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.Property(e => e.Title)
            //        .HasMaxLength(255)
            //        .IsRequired(); 

            //    entity.HasOne(c => c.Shop) 
            //        .WithMany(s => s.Categories)
            //        .HasForeignKey(c => c.ShopId)
            //        .IsRequired();

            //    entity.HasIndex(c => c.Title)
            //        .IsUnique();
            //});
        }
    }
}
