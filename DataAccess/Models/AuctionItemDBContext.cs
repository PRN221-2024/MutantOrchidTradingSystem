using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public partial class AuctionItemDbContext : DbContext
{
    public AuctionItemDbContext()
    {
    }

    public AuctionItemDbContext(DbContextOptions<AuctionItemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleAccount> RoleAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=AuctionItemDB;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC07D2796916");

            entity.ToTable("Account");

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
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC079F5EEB9F");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC07ABE00E15");

            entity.ToTable("Order");

            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Order_AccountId");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC07F24BDCD5");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderDetails_OrderId");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderDetails_ProductId");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07BCE63469");

            entity.ToTable("Product");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductCategory");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Category).WithMany()
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_ProductCategory_CategoryId");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductCategory_ProductId");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC075868C9D5");

            entity.ToTable("Role");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoleAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleAcco__3214EC0757C14F95");

            entity.ToTable("RoleAccount");

            entity.HasOne(d => d.Account).WithMany(p => p.RoleAccounts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_RoleAccount_AccountId");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleAccounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_AccountRole_RoleId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
