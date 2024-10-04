namespace HardcoreGamesRanking.Infrastructure.DirectorioDestino
{
    using Core.Dtos;
    using HardcoreGamesRanking.Core.Constants;
    using Infrastructure.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public partial class HardcoreGamesRankingContext(DbContextOptions<HardcoreGamesRankingContext> options, IConfiguration configuration) : DbContext(options)
    {
        #region Properties
        private readonly IConfiguration _configuration = configuration;
        private DataBaseDto? _dataBaseDto;
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");
           modelBuilder.ApplyAllConfigurations();

            base.OnModelCreating(modelBuilder);
        }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            DataBaseDto instance = _dataBaseDto = new DataBaseDto();
            _configuration.Bind(DataBaseConstant.DataBase, instance);
            var connectionString = _dataBaseDto.ConnectionString;
            if (!string.IsNullOrEmpty(connectionString)) optionsBuilder.UseSqlServer(connectionString);
        }
    }  
}