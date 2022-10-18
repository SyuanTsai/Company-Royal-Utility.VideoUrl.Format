using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using VideoUrlFormat.Domain.Server;

namespace VideoUrlFormat.Context
{
    public partial class ServerContext : DbContext
    {
        public ServerContext()
        {
        }

        public ServerContext(DbContextOptions<ServerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Server> Servers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server");

                entity.HasComment("伺服器(機台)");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("伺服器(機台)Id");

                entity.Property(e => e.Enable)
                    .HasDefaultValueSql("((1))")
                    .HasComment("是否啟用");

                entity.Property(e => e.GameNo).HasComment("遊戲Id");

                entity.Property(e => e.HistoryVideoUrl1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HistoryVideoURL1");

                entity.Property(e => e.HistoryVideoUrl2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HistoryVideoURL2");

                entity.Property(e => e.HistoryVideoUrl3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HistoryVideoURL3");

                entity.Property(e => e.LobbyNo).HasComment("館別Id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasComment("伺服器(機台)名稱");

            });

            modelBuilder.HasSequence("RXNumber")
                .HasMin(1)
                .HasMax(99999999)
                .IsCyclic();

            modelBuilder.HasSequence("TXNumber")
                .HasMin(1)
                .HasMax(99999999)
                .IsCyclic();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
