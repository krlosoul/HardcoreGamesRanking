namespace HardcoreGamesRanking.Infrastructure.Services
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Business.Interfaces.Services;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Core.Dtos;
    using Core.Constants;
    using Infrastructure.Extensions;

    public class JwtService : IJwtService
    {
        #region
        private readonly IConfiguration _configuration;
        private JwtDto? _jwtDto;
        #endregion

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            GetConfiguration();
        }

        public string GenerateToken(ClaimDto claimDto)
        {
            var claimsGenerator = ClaimsGenerator.Generate(claimDto);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtDto?.SecretKey ?? string.Empty);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsGenerator),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _jwtDto?.Issuer,
                Audience = _jwtDto?.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtDto?.SecretKey ?? string.Empty);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtDto?.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtDto?.Audience,
                    ValidateLifetime = true
                }, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ClaimDto GetDataByToken(string? token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validatedToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            var IdUsuario = validatedToken.Claims.First(c => c.Type == "IdUsuario").Value;

            return new ClaimDto { IdUsuario = Convert.ToInt32(IdUsuario) };
        }

        #region PrivateMethod
        private void GetConfiguration()
        {
            JwtDto instance = _jwtDto = new JwtDto();
            _configuration.Bind(JwtConstant.JwtConfig, instance);
            instance.SecretKey = _jwtDto.SecretKey;
            instance.Issuer = _jwtDto.Issuer;
            instance.Audience = _jwtDto.Audience;
        }
        #endregion
    }
}