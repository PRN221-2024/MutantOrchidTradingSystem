using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Models
{
    public partial class AuctionItemDBContext : DbContext
    {
        public AuctionItemDBContext()
        {
        }

        public AuctionItemDBContext(DbContextOptions<AuctionItemDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;
        public virtual DbSet<Photo> Photos { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleAccount> RoleAccounts { get; set; } = null!;
        public virtual DbSet<SlideShow> SlideShows { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnectionStr"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.InverseCategoryNavigation)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Category_ParentCategoryId");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.CustomerAddress).HasMaxLength(200);

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName).HasMaxLength(250);

                entity.Property(e => e.CustomerPhone).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK_InvoiceDetails_InvoiceId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_InvoiceDetails_ProductId");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Photo_ProductId");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product_CategoryId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleAccount>(entity =>
            {
                entity.ToTable("RoleAccount");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.RoleAccounts)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_RoleAccount_AccountId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleAccounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_RoleAccount_RoleId");
            });

            modelBuilder.Entity<SlideShow>(entity =>
            {
                entity.ToTable("SlideShow");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
