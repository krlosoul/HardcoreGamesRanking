namespace HardcoreGamesRanking.Infrastructure.DataAccess.Configurations
{
    using Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CalificacionConfiguration : IEntityTypeConfiguration<Calificacion>
    {
       public void Configure(EntityTypeBuilder<Calificacion> entity)
        {
            entity.ToTable("Calificaciones");
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nickname)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Puntuacion).HasColumnType("decimal(3, 2)");
            entity.HasOne(d => d.IdVideojuegoNavigation).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.IdVideojuego)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Calificaciones_Videojuegos");
            entity.HasOne(d => d.UsuarioActualizacionNavigation).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.UsuarioActualizacion)
                .HasConstraintName("FK_Calificaciones_Usuarios");
        }
    }
}