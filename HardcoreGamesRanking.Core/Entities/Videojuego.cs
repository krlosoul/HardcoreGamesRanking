namespace HardcoreGamesRanking.Core.Entities
{
    public partial class Videojuego
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int IdCompania { get; set; }

        public int AnioLanzamiento { get; set; }

        public decimal Precio { get; set; }

        public decimal Puntaje { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public int? UsuarioActualizacion { get; set; }
        
        public bool Activo { get; set; }

        public virtual ICollection<Calificacion> Calificaciones { get; set; } = [];

        public virtual Compania IdCompaniaNavigation { get; set; } = null!;

        public virtual Usuario? UsuarioActualizacionNavigation { get; set; }
    }
}