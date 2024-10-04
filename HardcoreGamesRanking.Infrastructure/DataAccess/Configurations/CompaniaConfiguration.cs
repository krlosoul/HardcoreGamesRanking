namespace HardcoreGamesRanking.Infrastructure.DataAccess.Configurations
{
    using Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CompaniaConfiguration : IEntityTypeConfiguration<Compania>
    {
       public void Configure(EntityTypeBuilder<Compania> entity)
        {
            entity.ToTable("Companias");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.HasOne(d => d.UsuarioActualizacionNavigation).WithMany(p => p.Compania)
                .HasForeignKey(d => d.UsuarioActualizacion)
                .HasConstraintName("FK_Companias_Usuarios");
        }
    }
}