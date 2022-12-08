using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data_Access_Layer
{
    public partial class HotelSystemContext : DbContext
    {
        public HotelSystemContext()
        {
        }

        public HotelSystemContext(DbContextOptions<HotelSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Branches> Branches { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<RoomType> RoomType { get; set; }
        public virtual DbSet<UserModel> UserModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HotelSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CurrentDate).HasColumnType("datetime");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(20, 2)");

                entity.Property(e => e.UserName).HasMaxLength(255);

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Booking__RoomId__30F848ED");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.UserName)
                    .HasConstraintName("FK__Booking__UserNam__31EC6D26");
            });

            modelBuilder.Entity<Branches>(entity =>
            {
                entity.HasKey(e => e.BranchId)
                    .HasName("PK__Branches__A1682FC5E023D8F2");

                entity.Property(e => e.BranchLocation)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.ImagePath).HasMaxLength(255);

                entity.Property(e => e.RoomPrice).HasColumnType("decimal(20, 2)");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Room__BranchID__25869641");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.RoomTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Room__RoomTypeId__267ABA7A");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__UserMode__536C85E5C8C34F34");

                entity.Property(e => e.Username).HasMaxLength(255);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
