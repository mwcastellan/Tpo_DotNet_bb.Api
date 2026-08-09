using Microsoft.EntityFrameworkCore;

namespace Tpo_DotNet_bb.Api.Api.Entities;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Vw_Pedidos> Vw_Pedidos { get; set; }

    public virtual DbSet<Vw_Productos> Vw_Productos { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Clientes> Clientes { get; set; }

    public virtual DbSet<Estado_Pedidos> Estado_Pedidos { get; set; }

    public virtual DbSet<Logs_Procesos> Logs_Procesos { get; set; }

    public virtual DbSet<Pedidos> Pedidos { get; set; }

    public virtual DbSet<Productos> Productos { get; set; }

    public virtual DbSet<Subcategoria> Subcategoria { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=mysql-mcastellan.alwaysdata.net;database=mcastellan_grp9;user=363082_grp9;password=CaC24127GRP9", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.18-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Vw_Pedidos>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_Pedidos");

            entity.Property(e => e.CANTIDAD).HasColumnType("int(11)");
            entity.Property(e => e.DESCRIPCION_CATEGORIA).HasMaxLength(100);
            entity.Property(e => e.DESCRIPCION_ESTADO_PEDIDOS).HasMaxLength(45);
            entity.Property(e => e.DESCRIPCION_PRODUCTO).HasMaxLength(100);
            entity.Property(e => e.DESCRIPCION_SUBCATEGORIA).HasMaxLength(100);
            entity.Property(e => e.EMAIL_CLIENTE).HasMaxLength(100);
            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.IDCLIENTE).HasColumnType("int(11)");
            entity.Property(e => e.IDESTADO)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)");
            entity.Property(e => e.IDPRODUCTO).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Vw_Productos>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_Productos");

            entity.Property(e => e.DESCRIPCION).HasMaxLength(100);
            entity.Property(e => e.DESCRIPCION_AMPLIA).HasMaxLength(200);
            entity.Property(e => e.DESCRIPCION_CATEGORIA).HasMaxLength(100);
            entity.Property(e => e.DESCRIPCION_SUBCATEGORIA).HasMaxLength(100);
            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.IDCATEGORIA).HasColumnType("int(11)");
            entity.Property(e => e.IDSUBCATEGORIA).HasColumnType("int(11)");
            entity.Property(e => e.NOMBRE_IMAGEN).HasMaxLength(45);
            entity.Property(e => e.PATH_CATEGORIA).HasMaxLength(100);
            entity.Property(e => e.PATH_SUBCATEGORIA).HasMaxLength(100);
            entity.Property(e => e.PRECIO).HasPrecision(10, 2);
            entity.Property(e => e.STOCK_DISPONIBLE).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");
            entity.HasIndex(e => e.ID, "ID_UNIQUE").IsUnique();
            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.DESCRIPCION).HasMaxLength(100);
            entity.Property(e => e.PATH_IMAGEN).HasMaxLength(100);
        });

        modelBuilder.Entity<Clientes>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");
            entity.HasIndex(e => e.EMAIL, "EMAIL_UNIQUE").IsUnique();
            entity.HasIndex(e => e.ID, "ID_UNIQUE").IsUnique();
            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.APELLIDO).HasMaxLength(45);
            entity.Property(e => e.DIRECCION).HasMaxLength(100);
            entity.Property(e => e.EMAIL).HasMaxLength(100);
            entity.Property(e => e.NOMBRE).HasMaxLength(45);
            entity.Property(e => e.PASSWORD).HasMaxLength(100);
        });

        modelBuilder.Entity<Estado_Pedidos>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");
            entity.HasIndex(e => e.ID, "id_UNIQUE").IsUnique();
            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.DESCRIPCION).HasMaxLength(45);
        });

        modelBuilder.Entity<Logs_Procesos>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");
            entity.HasIndex(e => e.ID, "ID_UNIQUE").IsUnique();
            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.MENSAJE).HasMaxLength(3000);
        });

        modelBuilder.Entity<Pedidos>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");
            entity.HasIndex(e => new { e.IDCLIENTE, e.ID }, "IDCLIENTE_ID_idx").IsUnique();
            entity.HasIndex(e => e.IDCLIENTE, "IDCLIENTE_idx");
            entity.HasIndex(e => e.IDESTADO, "IDESTADO_idx");
            entity.HasIndex(e => e.IDPRODUCTO, "IDPRODUCTO_idx");
            entity.HasIndex(e => e.ID, "ID_UNIQUE").IsUnique();
            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.CANTIDAD).HasColumnType("int(11)");
            entity.Property(e => e.FECHA_ENVIO).HasDefaultValueSql("current_timestamp()");
            entity.Property(e => e.IDCLIENTE).HasColumnType("int(11)");
            entity.Property(e => e.IDESTADO)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)");
            entity.Property(e => e.IDPRODUCTO).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Productos>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");
            entity.HasIndex(e => e.IDSUBCATEGORIA, "IDSUBCATEGORIA_idx");
            entity.HasIndex(e => e.ID, "ID_UNIQUE").IsUnique();
            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.DESCRIPCION).HasMaxLength(100);
            entity.Property(e => e.DESCRIPCION_AMPLIA).HasMaxLength(200);
            entity.Property(e => e.IDSUBCATEGORIA).HasColumnType("int(11)");
            entity.Property(e => e.NOMBRE_IMAGEN).HasMaxLength(45);
            entity.Property(e => e.PRECIO).HasPrecision(10, 2);
            entity.Property(e => e.STOCK_DISPONIBLE).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Subcategoria>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PRIMARY");
            entity.HasIndex(e => e.IDCATEGORIA, "IDCATEGORIA_idx");
            entity.HasIndex(e => e.ID, "ID_UNIQUE").IsUnique();
            entity.Property(e => e.ID).HasColumnType("int(11)");
            entity.Property(e => e.DESCRIPCION).HasMaxLength(100);
            entity.Property(e => e.IDCATEGORIA).HasColumnType("int(11)");
            entity.Property(e => e.PATH_IMAGEN).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
