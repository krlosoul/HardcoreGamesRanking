namespace HardcoreGamesRanking.Api.Configurations
{
    using Business;
    using Infrastructure;

    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddBusiness();
            services.AddCors(o => o.AddPolicy("AllowLocalOrigin", builder =>
            {
                builder.WithOrigins("http://127.0.0.1").AllowAnyMethod().AllowAnyHeader();
            }));

            return services;
        }
    }
}