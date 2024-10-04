namespace HardcoreGamesRanking.Infrastructure.Services
{
    using Business.Interfaces.Services;
    using Microsoft.AspNetCore.Http;

    public class CurrentUserService(IHttpContextAccessor httpContextAccessor, IJwtService jwtService) : ICurrentUserService
    {
        #region Parameters
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IJwtService _jwtService = jwtService;
        #endregion

        public int? GetCurrentIdUser()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token)) return null;
            var user = _jwtService.GetDataByToken(token);
            return user?.IdUsuario;
        }
    }
}