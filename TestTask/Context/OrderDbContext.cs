using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TestTask.Entities;

namespace TestTask.Context
{
    public partial class OrderDbContext : DbContext
    {
        public OrderDbContext()
        {
        }

        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<AvgCostByHours> AvgCostByHours { get; set; } = null!;
        public virtual DbSet<ClientsOrdersSumCostInBirthday> ClientsOrdersSumCostInBirthdayDbSet { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=order_db;Username=user;Password=password;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AvgCostByHours>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.AvgCost).HasColumnName("avg_cost");
                entity.Property(e => e.Hour).HasColumnName("hour");
            });

            modelBuilder.Entity<ClientsOrdersSumCostInBirthday>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.SummaryCost).HasColumnName("summary_cost");
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
            });


            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client", "order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BirthDate).HasColumnName("birth_date");

                entity.Property(e => e.FirstName).HasColumnName("first_name");

                entity.Property(e => e.LastName).HasColumnName("last_name");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order", "order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.OrderTime).HasColumnName("order_time");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_client_id_fkey");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_status_id_fkey");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("order_status", "order");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
