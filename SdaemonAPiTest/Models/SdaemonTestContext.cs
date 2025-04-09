using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SdaemonAPiTest.Models;

// DbContext class for interacting with the database
public partial class SdaemonTestContext : DbContext
{
    // Parameterless constructor - used when context is instantiated without options
    public SdaemonTestContext()
    {
    }

    // Constructor accepting DbContextOptions - allows external configuration (used in Dependency Injection)
    public SdaemonTestContext(DbContextOptions<SdaemonTestContext> options)
        : base(options)
    {
    }

    // DbSet represents the ToDo table in the database
    public virtual DbSet<ToDo> ToDos { get; set; }

    // Configure the database connection (only used when not configured externally)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-MN8K8AR;Database=SdaemonTest;Trusted_Connection=True;TrustServerCertificate=True;");
    // WARNING: It's not safe to keep connection strings in source code. Prefer storing it in appsettings.json or environment variables.

    // Configure entity mappings and relationships
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure ToDo entity/table
        modelBuilder.Entity<ToDo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ToDo__3214EC072EF59452"); // Define primary key

            entity.ToTable("ToDo"); // Map entity to table "ToDo"

            entity.Property(e => e.DueDate).HasColumnType("datetime"); // Configure DueDate column type
            entity.Property(e => e.Title).HasMaxLength(100); // Limit Title column to 100 characters
        });

        OnModelCreatingPartial(modelBuilder); // Allows extending model configuration in another partial class
    }

    // Partial method stub for additional model configuration (can be defined elsewhere)
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
