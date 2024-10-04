namespace HardcoreGamesRanking.Infrastructure
{
    using Business.Interfaces.DataAccess;
    using Business.Interfaces.Services;
    using Infrastructure.Services;
    using Infrastructure.DataAccess;
    using Infrastructure.DirectorioDestino;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<HardcoreGamesRankingContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
