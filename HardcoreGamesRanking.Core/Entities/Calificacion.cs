namespace HardcoreGamesRanking.Core.Entities
{
    public partial class Calificacion
    {
        public Guid Id { get; set; }

        public string Nickname { get; set; } = null!;

        public int IdVideojuego { get; set; }

        public decimal Puntuacion { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public int? UsuarioActualizacion { get; set; }

        public virtual Videojuego IdVideojuegoNavigation { get; set; } = null!;

        public virtual Usuario? UsuarioActualizacionNavigation { get; set; }
    }
}