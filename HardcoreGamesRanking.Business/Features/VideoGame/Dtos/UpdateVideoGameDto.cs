namespace HardcoreGamesRanking.Business.Features.VideoGame.Dtos
{
    public class UpdateVideoGameDto : CreateVideoGameDto
    {
        public int? IdVideoGame { get; set; }
    }
}