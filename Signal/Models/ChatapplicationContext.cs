using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Signal.Models;

public partial class ChatapplicationContext : DbContext
{
    public ChatapplicationContext()
    {
    }

    public ChatapplicationContext(DbContextOptions<ChatapplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PC0382\\MSSQL2019;Database=chatapplication;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.Convid).HasName("PK__conversa__0F9C3C5D364CC619");

            entity.ToTable("conversation");

            entity.Property(e => e.Convid).HasColumnName("convid");
            entity.Property(e => e.User1).HasColumnName("user1");
            entity.Property(e => e.User2).HasColumnName("user2");

            entity.HasOne(d => d.User1Navigation).WithMany(p => p.ConversationUser1Navigations)
                .HasForeignKey(d => d.User1)
                .HasConstraintName("fkuser1");

            entity.HasOne(d => d.User2Navigation).WithMany(p => p.ConversationUser2Navigations)
                .HasForeignKey(d => d.User2)
                .HasConstraintName("fkuser2");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Msgid).HasName("PK__message__A95DB69FD219032C");

            entity.ToTable("message");

            entity.Property(e => e.Msgid).HasColumnName("msgid");
            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.Convid).HasColumnName("convid");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.Author)
                .HasConstraintName("fkauthor");

            entity.HasOne(d => d.Conv).WithMany(p => p.Messages)
                .HasForeignKey(d => d.Convid)
                .HasConstraintName("fkconvid");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PK__users__CBA1B257E8910A81");

            entity.ToTable("users");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.Userpass)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("userpass");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
