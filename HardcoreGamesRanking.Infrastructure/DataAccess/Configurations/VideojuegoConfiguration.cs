namespace HardcoreGamesRanking.Infrastructure.DataAccess.Configurations
{
    using Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class VideojuegoConfiguration : IEntityTypeConfiguration<Videojuego>
    {
       public void Configure(EntityTypeBuilder<Videojuego> entity)
        {
            entity.ToTable("Videojuegos");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Puntaje).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.Activo)
              .IsRequired()
              .HasDefaultValue(true);
            entity.HasOne(d => d.IdCompaniaNavigation).WithMany(p => p.Videojuegos)
                .HasForeignKey(d => d.IdCompania)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Videojuegos_Companias");

            entity.HasOne(d => d.UsuarioActualizacionNavigation).WithMany(p => p.Videojuegos)
                .HasForeignKey(d => d.UsuarioActualizacion)
                .HasConstraintName("FK_Videojuegos_Usuarios");
        }
    }
}