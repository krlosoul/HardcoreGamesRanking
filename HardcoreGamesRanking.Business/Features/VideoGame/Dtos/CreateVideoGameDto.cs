namespace HardcoreGamesRanking.Business.Features.VideoGame.Dtos
{
    public class CreateVideoGameDto
    {
        public string? Nombre { get; set; }

        public int? IdCompania { get; set; }

        public int? AnioLanzamiento { get; set; }

        public decimal? Precio { get; set; }

        public decimal? Puntaje { get; set; }
    }
}