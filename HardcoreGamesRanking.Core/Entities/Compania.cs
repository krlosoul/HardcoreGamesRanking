namespace HardcoreGamesRanking.Core.Entities
{
    public partial class Compania
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public int? UsuarioActualizacion { get; set; }

        public virtual Usuario? UsuarioActualizacionNavigation { get; set; }

        public virtual ICollection<Videojuego> Videojuegos { get; set; } = [];
    }
}
