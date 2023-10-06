using Microsoft.EntityFrameworkCore;
using WpfForM_CRM.Entities;

namespace WpfForM_CRM.Context
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users => Set<User>();
        public DbSet<Shop> Shops => Set<Shop>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ChildCategory> ChildCategories => Set<ChildCategory>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<CashRegister> CashRegisters => Set<CashRegister>();
        
        //public DbSet<Stock> Stocks => Set<Stock>();

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
                entity.HasMany(user => user.Shops)
                    .WithOne(shop => shop.User)
                    .HasForeignKey(shop => shop.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(shop => shop.Categories)
                    .WithOne(category => category.Shop)
                    .HasForeignKey(shop => shop.ShopId)
                    .OnDelete(DeleteBehavior.Cascade);

                ////entity.HasIndex(shop => shop.Name).IsUnique();
            });

            modelBuilder.Entity<Category>(category =>
            {
                category.HasKey(c => c.Id);
                category.HasMany(c => c.ChildCategories)
                    .WithOne(ch => ch.Category)
                    .HasForeignKey(ch => ch.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<ChildCategory>(child =>
            {
                child.HasKey(ch => ch.Id);

                child.HasMany(c => c.Products)
                    .WithOne(ch => ch.ChildCategory)
                    .HasForeignKey(ch => ch.ChildCategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<CashRegister>()
                .HasKey(p => p.Id);

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
