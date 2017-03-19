using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using arfaWeb.Database;

namespace arfa.Repository.EfSql.Database.Database
{
    public partial class arfaDBContext : DbContext
    {
        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<GameResult> GameResult { get; set; }
        public virtual DbSet<GameScore> GameScore { get; set; }
        public virtual DbSet<Table> Table { get; set; }
        public virtual DbSet<TableState> TableState { get; set; }
        public virtual DbSet<TableUser> TableUser { get; set; }
        public virtual DbSet<TableUserCard> TableUserCard { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserTableInventory> UserTableInventory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=arfaDB;Trusted_Connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.CardAlias)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CardName)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<GameResult>(entity =>
            {
                entity.HasKey(e => new { e.WinnerUserId, e.TableId })
                    .HasName("PK__GameResu__A40CD326F2BAF693");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.GameResult)
                    .HasForeignKey(d => d.TableId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_GameResult_ToTable");

                entity.HasOne(d => d.WinnerUser)
                    .WithMany(p => p.GameResult)
                    .HasForeignKey(d => d.WinnerUserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_GameResult_ToUser");
            });

            modelBuilder.Entity<GameScore>(entity =>
            {
                entity.HasKey(e => new { e.TableId, e.UserId })
                    .HasName("PK__GameScor__AC278D2AF2092CB1");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.GameScore)
                    .HasForeignKey(d => d.TableId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_GameScore_Table");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GameScore)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_GameScore_User");
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.CurrentSuit).HasColumnType("varchar(10)");

                entity.Property(e => e.GameStarted).HasDefaultValueSql("0");

                entity.Property(e => e.HandsAccumulated).HasDefaultValueSql("0");

                entity.Property(e => e.LastWinnerTimestamp).HasColumnType("datetime");

                entity.Property(e => e.Suspended).HasDefaultValueSql("0");

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Timestamp).HasColumnType("varchar(200)");

                entity.Property(e => e.Trump).HasColumnType("varchar(10)");

                entity.Property(e => e.TurnStart).HasDefaultValueSql("1");

                entity.HasOne(d => d.LastWinnerUser)
                    .WithMany(p => p.TableLastWinnerUser)
                    .HasForeignKey(d => d.LastWinnerUserId)
                    .HasConstraintName("FK_Table_User1");

                entity.HasOne(d => d.OwnerUser)
                    .WithMany(p => p.TableOwnerUser)
                    .HasForeignKey(d => d.OwnerUserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_User_Table");
            });

            modelBuilder.Entity<TableState>(entity =>
            {
                entity.HasKey(e => new { e.CardId, e.UserId, e.TableId })
                    .HasName("PK__TableSta__6BFB1E6B4375EE38");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.TableState)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TableState_ToCard");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.TableState)
                    .HasForeignKey(d => d.TableId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TableState_ToTable");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TableState)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TableState_ToUser");
            });

            modelBuilder.Entity<TableUser>(entity =>
            {
                entity.Property(e => e.IsDominant).HasDefaultValueSql("0");

                entity.Property(e => e.Score).HasDefaultValueSql("0");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.TableUser)
                    .HasForeignKey(d => d.TableId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TableUser_ToTable");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TableUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TableUser_ToUser");
            });

            modelBuilder.Entity<TableUserCard>(entity =>
            {
                entity.HasKey(e => new { e.TableUserId, e.CardId })
                    .HasName("PK__TableUse__A8AD0EFA3734BD73");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.TableUserCard)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TableUserCard_Card");

                entity.HasOne(d => d.TableUser)
                    .WithMany(p => p.TableUserCard)
                    .HasForeignKey(d => d.TableUserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TableUserCard_TableUser");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .HasName("UQ_User_Username")
                    .IsUnique();

                entity.Property(e => e.Firstname).HasColumnType("varchar(50)");

                entity.Property(e => e.Lastname).HasColumnType("varchar(50)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.TokenLastHit).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<UserTableInventory>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CardId, e.TableId })
                    .HasName("PK__UserTabl__0DAA7F97EB91B6E8");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.UserTableInventory)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserTableInventory_Card");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.UserTableInventory)
                    .HasForeignKey(d => d.TableId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserTableInventory_Table");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTableInventory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserTableInventory_User");
            });
        }
    }
}