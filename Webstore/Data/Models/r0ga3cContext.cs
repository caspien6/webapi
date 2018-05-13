using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Webstore.Data.Models
{
    public partial class R0ga3cContext : IdentityDbContext<ApplicationUser>
    {
        public R0ga3cContext(DbContextOptions<R0ga3cContext> options)
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-PN7VT3L\SQLEXPRESS;Initial Catalog=r0ga3c;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                
            }
        }

        public virtual DbSet<Kategoria> Kategoria { get; set; }
        public virtual DbSet<Kosar> Kosar { get; set; }
        public virtual DbSet<KosarTetel> KosarTetel { get; set; }
        public virtual DbSet<Statusz> Statusz { get; set; }
        public virtual DbSet<Termek> Termek { get; set; }
        public virtual DbSet<Vevo> Vevo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Kategoria>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nev).HasMaxLength(50);

                entity.HasOne(d => d.AlkategoriaNavigation)
                    .WithMany(p => p.InverseAlkategoriaNavigation)
                    .HasForeignKey(d => d.Alkategoria)
                    .HasConstraintName("FK__Kategoria__Szulo__2645B050");
            });

            modelBuilder.Entity<Kosar>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.Property(e => e.StatuszId).HasColumnName("StatuszID");

                entity.Property(e => e.TelephelyId).HasColumnName("TelephelyID");

                entity.Property(e => e.VevoId).HasColumnName("VevoID");

                entity.HasOne(d => d.Statusz)
                    .WithMany(p => p.Kosar)
                    .HasForeignKey(d => d.StatuszId)
                    .HasConstraintName("FK__Kosar__Statu__339FAB6E");

                entity.HasOne(d => d.Vevo)
                    .WithMany(p => p.Kosar)
                    .HasForeignKey(d => d.VevoId)
                    .HasConstraintName("FK_Kosar_Vevo");
            });

            modelBuilder.Entity<KosarTetel>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.KosarId).HasColumnName("KosarID");

                entity.Property(e => e.StatuszId).HasColumnName("StatuszID");

                entity.Property(e => e.TermekId).HasColumnName("TermekID");

                entity.HasOne(d => d.Kosar)
                    .WithMany(p => p.KosarTetel)
                    .HasForeignKey(d => d.KosarId)
                    .HasConstraintName("FK__Kosar__KosarTetel__37703C52");

                entity.HasOne(d => d.Statusz)
                    .WithMany(p => p.KosarTetel)
                    .HasForeignKey(d => d.StatuszId)
                    .HasConstraintName("FK__Megrendel__Statu__395884C4");

                entity.HasOne(d => d.Termek)
                    .WithMany(p => p.KosarTetel)
                    .HasForeignKey(d => d.TermekId)
                    .HasConstraintName("FK__Megrendel__Terme__3864608B");
            });

            modelBuilder.Entity<Statusz>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nev).HasMaxLength(20);
            });

            modelBuilder.Entity<Termek>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.KategoriaId).HasColumnName("KategoriaID");

                entity.Property(e => e.KepUrl).HasMaxLength(200);

                entity.Property(e => e.Nev).HasMaxLength(50);

                entity.Property(e => e.Views).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Kategoria)
                    .WithMany(p => p.Termek)
                    .HasForeignKey(d => d.KategoriaId)
                    .HasConstraintName("FK__Termek__Kategori__2A164134");
            });

            modelBuilder.Entity<Vevo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Jelszo).HasMaxLength(50);

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.Nev).HasMaxLength(50);

                entity.Property(e => e.Szamlaszam)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
