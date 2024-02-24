using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Bid> Bids { get; set; } = null!;
        public virtual DbSet<Buyer> Buyers { get; set; } = null!;
        public virtual DbSet<BuyerCommunication> BuyerCommunications { get; set; } = null!;
        public virtual DbSet<BuyerFeedback> BuyerFeedbacks { get; set; } = null!;
        public virtual DbSet<BuyerProblem> BuyerProblems { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Photo> Photos { get; set; } = null!;
        public virtual DbSet<Seller> Sellers { get; set; } = null!;
        public virtual DbSet<SellerCommunication> SellerCommunications { get; set; } = null!;
        public virtual DbSet<SellerFeedback> SellerFeedbacks { get; set; } = null!;
        public virtual DbSet<SellerProblem> SellerProblems { get; set; } = null!;
        public virtual DbSet<Ship> Ships { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=AuctionItemDB;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("ADMIN");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ADMIN__User_Id__6383C8BA");
            });

            modelBuilder.Entity<Bid>(entity =>
            {
                entity.ToTable("BIDS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BidAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.BidTime).HasColumnType("datetime");

                entity.Property(e => e.BuyerId).HasColumnName("BuyerID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK__BIDS__BuyerID__6477ECF3");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__BIDS__ItemID__656C112C");
            });

            modelBuilder.Entity<Buyer>(entity =>
            {
                entity.ToTable("BUYER");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("User_Id");
            });

            modelBuilder.Entity<BuyerCommunication>(entity =>
            {
                entity.ToTable("BUYER_COMMUNICATION");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BuyerId).HasColumnName("BuyerID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Message).IsUnicode(false);

                entity.Property(e => e.SellerId).HasColumnName("SellerID");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.BuyerCommunications)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK__BUYER_COM__Buyer__66603565");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.BuyerCommunications)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__BUYER_COM__Selle__6754599E");
            });

            modelBuilder.Entity<BuyerFeedback>(entity =>
            {
                entity.ToTable("BUYER_FEEDBACKS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BuyerId).HasColumnName("BuyerID");

                entity.Property(e => e.Comment).IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.SellerId).HasColumnName("SellerID");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.BuyerFeedbacks)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK__BUYER_FEE__Buyer__68487DD7");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.BuyerFeedbacks)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__BUYER_FEE__Selle__693CA210");
            });

            modelBuilder.Entity<BuyerProblem>(entity =>
            {
                entity.ToTable("BUYER_PROBLEM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.BuyerId).HasColumnName("BuyerID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.BuyerProblems)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK__BUYER_PRO__Admin__6A30C649");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.BuyerProblems)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK__BUYER_PRO__Buyer__6B24EA82");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("ITEM");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ArrivalDate).HasColumnType("date");

                entity.Property(e => e.AuctionEnd).HasColumnType("datetime");

                entity.Property(e => e.AuctionStart).HasColumnType("datetime");

                entity.Property(e => e.BuyerId).HasColumnName("BuyerID");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Condition)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.SellerId).HasColumnName("SellerID");

                entity.Property(e => e.ShipDate).HasColumnType("date");

                entity.Property(e => e.ShipPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.StartBidAmount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK__ITEM__BuyerID__6C190EBB");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__ITEM__SellerID__6D0D32F4");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PHOTO");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.Photo1)
                    .IsUnicode(false)
                    .HasColumnName("Photo");

                entity.HasOne(d => d.Item)
                    .WithMany()
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK__PHOTO__ItemID__6E01572D");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.ToTable("SELLER");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Sellers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__SELLER__User_Id__6EF57B66");
            });

            modelBuilder.Entity<SellerCommunication>(entity =>
            {
                entity.ToTable("SELLER_COMMUNICATION");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BuyerId).HasColumnName("BuyerID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Message).IsUnicode(false);

                entity.Property(e => e.SellerId).HasColumnName("SellerID");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.SellerCommunications)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK__SELLER_CO__Buyer__6FE99F9F");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.SellerCommunications)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__SELLER_CO__Selle__70DDC3D8");
            });

            modelBuilder.Entity<SellerFeedback>(entity =>
            {
                entity.ToTable("SELLER_FEEDBACK");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BuyerId).HasColumnName("BuyerID");

                entity.Property(e => e.Comment).IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.SellerId).HasColumnName("SellerID");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.SellerFeedbacks)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK__SELLER_FE__Buyer__71D1E811");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.SellerFeedbacks)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK__SELLER_FE__Selle__72C60C4A");
            });

            modelBuilder.Entity<SellerProblem>(entity =>
            {
                entity.ToTable("SELLER_PROBLEMS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.BuyerId).HasColumnName("BuyerID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.SellerProblems)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK__SELLER_PR__Admin__73BA3083");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.SellerProblems)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK__SELLER_PR__Buyer__74AE54BC");
            });

            modelBuilder.Entity<Ship>(entity =>
            {
                entity.ToTable("SHIPS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ArrivalDate).HasColumnType("date");

                entity.Property(e => e.ShipDate).HasColumnType("date");

                entity.Property(e => e.ShipPrice).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserTypeNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserType)
                    .HasConstraintName("FK__USER__UserType__75A278F5");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("USER_TYPE");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
