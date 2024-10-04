namespace HardcoreGamesRanking.Business.Features.VideoGame.Dtos
{
    using Core.Dtos;

    public class GetAllVideoGameFilterDto : PaginationDto
    {
        public string? Nombre { get; set; }

        public int? IdCompania { get; set; }

        public int? AnioLanzamiento { get; set; }
    }
}