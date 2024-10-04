namespace HardcoreGamesRanking.Business.Features.VideoGame.Dtos
{
    public class GetAllVideoGameDto
    {
        public string Nombre { get; set; } = null!;

        public string Compania { get; set; } = null!;

        public int Anio { get; set; }

        public decimal Precio { get; set; }
    }
}