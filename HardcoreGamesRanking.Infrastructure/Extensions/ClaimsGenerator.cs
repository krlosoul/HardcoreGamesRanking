namespace HardcoreGamesRanking.Infrastructure.Extensions
{
    using Core.Dtos;
    using System.Collections.Generic;
    using System.Security.Claims;

    public static class ClaimsGenerator
    {
        public static IEnumerable<Claim> Generate(ClaimDto claimDto)
        {
            var claims = new List<Claim>();
            if (claimDto?.IdUsuario != null) claims.Add(new Claim("IdUsuario", claimDto.IdUsuario.ToString()));

            return claims;
        }
    }
}