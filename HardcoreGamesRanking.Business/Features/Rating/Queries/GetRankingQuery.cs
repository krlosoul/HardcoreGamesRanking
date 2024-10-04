namespace HardcoreGamesRanking.Business.Features.Rating.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using Business.Features.Rating.Dtos;
    using Core.Dtos;
    using Business.Interfaces.DataAccess;
    using MediatR;
    using System.Linq.Expressions;
    using Core.Entities;
    using HardcoreGamesRanking.Business.Common.Exceptions;
    using System.Text;

    public class GetRankingQuery : GetRankingDto, IRequest<ResponseDto<string>> {}

    public class GetRankingQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRankingQuery, ResponseDto<string>>
    {
        #region Parameters
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        #endregion

        public async Task<ResponseDto<string>> Handle(GetRankingQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Videojuego, bool>> where = v => v.Activo;
            var includeConfigurations = new (Expression<Func<Videojuego, object>> Include, Expression<Func<object, object>>? ThenInclude)[]
            {
                (v => v.IdCompaniaNavigation, null),
                (v => v.Calificaciones, null)
            };
            var (videojuegos, totalRecords) = await _unitOfWork.VideojuegoRepository.GetAllAsync(where: where, includeProperties: includeConfigurations);
            if (!videojuegos.Any()) throw new NotFoundException("No hay videojuegos en el sistema.");
            var ranking = videojuegos
                .Select(v => new
                {
                    v.Nombre,
                    Compania = v.IdCompaniaNavigation.Nombre,
                    PuntajePromedio = v.Calificaciones.Any() ? v.Calificaciones.Average(c => (decimal?)c.Puntuacion) ?? 0  : 0,
                })
                .OrderByDescending(v => v.PuntajePromedio) 
                .Take(request.Top ?? 20)
                .AsEnumerable()
                .Select((v, index) => new
                {
                    v.Nombre,
                    v.Compania,
                    v.PuntajePromedio,
                    Clasificacion = (index + 1) <= (request.Top ?? 20 / 2) ? "GOTY" : "AAA"
                })
                .ToList();

                var csv = new StringBuilder();
                csv.AppendLine("Nombre|Compania|Puntaje|Clasificacion");

                foreach (var videojuego in ranking)
                {
                    csv.AppendLine($"{videojuego.Nombre}|{videojuego.Compania}|{videojuego.PuntajePromedio:F2}|{videojuego.Clasificacion}");
                }

                var response = new ResponseDto<string>
                {
                    Data = csv.ToString(),
                    Message = "Éxito"
                };
            
            return response; 
        }
    }
}