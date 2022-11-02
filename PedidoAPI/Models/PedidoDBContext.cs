using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PedidoAPI.Models
{
    public partial class PedidoDBContext : DbContext
    {
        public PedidoDBContext()
        {
        }

        public PedidoDBContext(DbContextOptions<PedidoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ItensPedido> ItensPedidos { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<Produto> Produtos { get; set; } = null!;

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItensPedido>(entity =>
            {
                entity.ToTable("ItensPedido");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.ItensPedidos)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Pedido");

                entity.HasOne(d => d.IdProdutoNavigation)
                    .WithMany(p => p.ItensPedidos)
                    .HasForeignKey(d => d.IdProduto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Produto");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("Pedido");

                entity.Property(e => e.DataCriacao).HasColumnType("datetime");

                entity.Property(e => e.EmailCliente)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.NomeCliente)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.ToTable("Produto");

                entity.Property(e => e.NomeProduto)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
