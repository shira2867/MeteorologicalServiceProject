using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dal.DataBase.models;

public partial class dbmanager : DbContext
{
    public dbmanager()
    {
    }

    public dbmanager(DbContextOptions<dbmanager> options)
        : base(options)
    {
    }

    public virtual DbSet<GetCalculateDatum> GetCalculateData { get; set; }


    public virtual DbSet<Station> Stations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\משתמש\\Documents\\Station.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GetCalculateDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GetCalcu__3214EC0772046EFF");

            entity.Property(e => e.NameStation)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

      

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Station__3214EC07F98B17EE");

            entity.ToTable("Station");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Adress)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.Principal)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
