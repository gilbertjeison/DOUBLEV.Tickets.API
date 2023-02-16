using DataAccess.Common.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataAccess.Common;

public partial class TicketsContext : DbContext, IMainContext
{
    public TicketsContext()
    {
    }

    public TicketsContext(DbContextOptions<TicketsContext> options)
        : base(options)
    {
    }

    public TicketsContext(string connectionString) : base(GetOptions(connectionString, null)) { }

    public TicketsContext(string connectionString, Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction) 
        : base(GetOptions(connectionString, sqlServerOptionsAction)) { }


    public virtual DbSet<Ticket> Tickets { get; set; }

    private static DbContextOptions GetOptions(string connectionString, Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction)
    {
        return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString, sqlServerOptionsAction).Options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("ticket");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("user_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
