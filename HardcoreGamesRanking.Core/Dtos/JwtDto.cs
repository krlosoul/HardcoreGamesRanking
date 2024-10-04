namespace HardcoreGamesRanking.Core.Dtos
{
    public class JwtDto
    {
        public string? SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}