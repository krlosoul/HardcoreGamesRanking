namespace HardcoreGamesRanking.Business.Features.User.Dtos
{
    public class CreateUserDto
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}