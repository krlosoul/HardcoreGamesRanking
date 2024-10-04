namespace HardcoreGamesRanking.Business.Features.User.Dtos
{
    public class AuthenticateDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}