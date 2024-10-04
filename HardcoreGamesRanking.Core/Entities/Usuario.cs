namespace HardcoreGamesRanking.Core.Entities
{
    public partial class Usuario
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }

        public virtual ICollection<Calificacion> Calificaciones { get; set; } = [];

        public virtual ICollection<Compania> Compania { get; set; } = [];

        public virtual ICollection<Videojuego> Videojuegos { get; set; } = [];
    }
}