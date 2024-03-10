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

    public virtual DbSet<Auction> Auctions { get; set; }

    public virtual DbSet<Bid> Bids { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleAccount> RoleAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=sa123456;database=AuctionItemDB;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC0739685ECA");

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

        modelBuilder.Entity<Auction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Auction__3214EC07B14EFBE4");

            entity.ToTable("Auction");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.StartingPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Auction__Product__46E78A0C");
        });

        modelBuilder.Entity<Bid>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bid__3214EC07C26BA65F");

            entity.ToTable("Bid");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BidTime).HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.Bids)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Bid__AccountId__47DBAE45");

            entity.HasOne(d => d.Auction).WithMany(p => p.Bids)
                .HasForeignKey(d => d.AuctionId)
                .HasConstraintName("FK__Bid__AuctionId__48CFD27E");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC071A8E831B");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC071B65C0E7");

            entity.ToTable("Order");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Order_AccountId");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC07C183971A");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderDetails_OrderId");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderDetails_ProductId");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07311BE031");

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
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07D575B9A2");

            entity.ToTable("Role");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoleAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleAcco__3214EC07FBE1C24E");

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
