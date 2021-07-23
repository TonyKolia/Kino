using System;
using System.IO;
using Kino.API.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Kino.Models.KinoDBModel
{
    public partial class KinoDBContext : DbContext
    {
        public KinoDBContext()
        {
        }

        public KinoDBContext(DbContextOptions<KinoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bet> Bets { get; set; }
        public virtual DbSet<Draw> Draws { get; set; }
        public virtual DbSet<Outcome> Outcomes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserCategory> UserCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationHelper.GetConfiguration().GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bet>(entity =>
            {
                entity.Property(e => e.BetAmount)
                    .HasColumnType("decimal(19, 3)")
                    .HasColumnName("bet_amount");

                entity.Property(e => e.BetDate)
                    .HasColumnType("datetime")
                    .HasColumnName("bet_date");

                entity.Property(e => e.DrawId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("draw_id");

                entity.Property(e => e.NoOfNumbers).HasColumnName("no_of_numbers");

                entity.Property(e => e.Outcome).HasColumnName("outcome");

                entity.Property(e => e.SelectedNumbers)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("selected_numbers");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<Draw>(entity =>
            {
                entity.Property(e => e.DrawId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("draw_id");

                entity.Property(e => e.DrawDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("draw_date_time");

                entity.Property(e => e.KinoBonus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("kino_bonus");

                entity.Property(e => e.WinningNumbers)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("winning_numbers");
            });

            modelBuilder.Entity<Outcome>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Birthdate)
                    .HasColumnType("datetime")
                    .HasColumnName("birthdate");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasColumnName("register_date");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<UserCategory>(entity =>
            {
                entity.ToTable("User_Categories");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
