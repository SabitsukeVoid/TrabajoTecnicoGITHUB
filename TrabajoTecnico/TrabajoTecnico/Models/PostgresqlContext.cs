using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrabajoTecnico.Models;

public partial class PostgresqlContext : DbContext
{
    public PostgresqlContext()
    {
    }

    public PostgresqlContext(DbContextOptions<PostgresqlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetalleProducto> DetalleProductos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-6PT8B7C; DataBase=POSTGRESQL;Integrated Security=True;Persist Security Info=False;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetalleProducto>(entity =>
        {
            entity.HasKey(e => e.IdDetalleProducto).HasName("PK__DetalleP__49CD10374A76C3D8");

            entity.Property(e => e.IdDetalleProducto)
                .HasMaxLength(15)
                .HasColumnName("idDetalleProducto");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto)
                .HasMaxLength(15)
                .HasColumnName("idProducto");
            entity.Property(e => e.ValorIva).HasColumnName("valorIva");
            entity.Property(e => e.ValorTotal).HasColumnName("valorTotal");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleProductos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("idProducto");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__07F4A1325C726F3E");

            entity.Property(e => e.IdProducto)
                .HasMaxLength(15)
                .HasColumnName("idProducto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(75)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio).HasColumnName("precio");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
