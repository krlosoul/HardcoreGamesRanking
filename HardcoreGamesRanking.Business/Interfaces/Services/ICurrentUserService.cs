namespace HardcoreGamesRanking.Business.Interfaces.Services
{
    public interface ICurrentUserService
    {
        /// <summary>
        /// Get current id user.
        /// </summary>
        /// <returns>IdUser.</returns>
        public int? GetCurrentIdUser();
    }
}