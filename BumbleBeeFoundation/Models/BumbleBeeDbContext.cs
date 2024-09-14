using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BumbleBeeFoundation.Models;

public partial class BumbleBeeDbContext : DbContext
{
    public BumbleBeeDbContext()
    {
    }

    public BumbleBeeDbContext(DbContextOptions<BumbleBeeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<DonationSar> DonationSars { get; set; }

    public virtual DbSet<FundingRequest> FundingRequests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=labVMH8OX\\SQLEXPRESS;Initial Catalog=BumbleBeeDB;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Companie__2D971C4CB9435990");

            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DateJoined)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Pending");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF6FFE39910F");

            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DocumentType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UploadDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.Documents)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Documents__Compa__59FA5E80");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PK__Donation__C5082EDB9E661A06");

            entity.Property(e => e.DonationId).HasColumnName("DonationID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.DonationAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DonationType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DonorEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DonorIdnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DonorIDNumber");
            entity.Property(e => e.DonorName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DonorPhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DonorTaxNumber)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.Donations)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Donations__Compa__52593CB8");
        });

        modelBuilder.Entity<DonationSar>(entity =>
        {
            entity.HasKey(e => e.Sarsid).HasName("PK__Donation__7358C06BC8F1F16B");

            entity.ToTable("DonationSARS");

            entity.Property(e => e.Sarsid).HasColumnName("SARSID");
            entity.Property(e => e.DonationId).HasColumnName("DonationID");
            entity.Property(e => e.GeneratedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Sarsdocument)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SARSDocument");

            entity.HasOne(d => d.Donation).WithMany(p => p.DonationSars)
                .HasForeignKey(d => d.DonationId)
                .HasConstraintName("FK__DonationS__Donat__5EBF139D");
        });

        modelBuilder.Entity<FundingRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__FundingR__33A8519AB9FA5969");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.ProjectDescription).HasColumnType("text");
            entity.Property(e => e.ProjectImpact).HasColumnType("text");
            entity.Property(e => e.RequestedAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Pending");
            entity.Property(e => e.SubmittedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.FundingRequests)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__FundingRe__Compa__5535A963");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACE6AAA44F");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534AFC8AFF7").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
