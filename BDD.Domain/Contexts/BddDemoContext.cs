using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BDD.Domain.Models;

#nullable disable

namespace BDD.Domain.Contexts
{
    public partial class BddDemoContext : DbContext
    {
        public BddDemoContext()
        {
        }

        public BddDemoContext(DbContextOptions<BddDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Todo> Todos { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=bdd_demo;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.utf8");

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notification");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsDismissed).HasColumnName("IS_DISMISSED");

                entity.Property(e => e.TodoId).HasColumnName("TODO_ID");

                entity.HasOne(d => d.Todo)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.TodoId)
                    .HasConstraintName("notification_todo_id_fk");
            });

            modelBuilder.Entity<Todo>(entity =>
            {
                entity.ToTable("todo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("CREATED_DATE");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("DELETED_DATE");

                entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");

                entity.Property(e => e.TargetDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("TARGET_DATE");

                entity.Property(e => e.Text).HasColumnName("TEXT");

                entity.Property(e => e.Title).HasColumnName("TITLE");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Todos)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("todo_user_id_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasColumnName("EMAIL");

                entity.Property(e => e.IsDeleted).HasColumnName("IS_DELETED");

                entity.Property(e => e.Preferences).HasColumnName("PREFERENCES");

                entity.Property(e => e.Username).HasColumnName("USERNAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
