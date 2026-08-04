using Microsoft.EntityFrameworkCore;
using Tpo_DotNet_bb.Api.Api.Entities;

namespace Tpo_DotNet_bb.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Clientes> Clientes => Set<Clientes>();
    public DbSet<Pedidos> Pedidos => Set<Pedidos>();
    public DbSet<Productos> Productos => Set<Productos>();
    public DbSet<Subcategoria> Subcategorias => Set<Subcategoria>();
    public DbSet<Estado_Pedidos> EstadoPedidos => Set<Estado_Pedidos>();
    public DbSet<Logs_Procesos> LogsProcesos => Set<Logs_Procesos>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clientes>()
            .ToTable("clientes");

        modelBuilder.Entity<Pedidos>()
            .ToTable("pedidos");

        modelBuilder.Entity<Productos>()
            .ToTable("productos");
        modelBuilder.Entity<Subcategoria>()
            .ToTable("subcategoria");

        modelBuilder.Entity<Estado_Pedidos>()
            .ToTable("estado_pedidos");

        modelBuilder.Entity<Logs_Procesos>()
            .ToTable("logs_procesos");
    }
}