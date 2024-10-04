namespace HardcoreGamesRanking.Business.Features.VideoGame.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Business.Features.VideoGame.Dtos;
    using Core.Dtos;
    using Business.Interfaces.DataAccess;
    using MediatR;
    using Business.Common.Exceptions;
    using Core.Entities;
    using System.Linq.Expressions;

    public class GetAllVideoGameFilterQuery : GetAllVideoGameFilterDto, IRequest<ResponseDto<IEnumerable<GetAllVideoGameDto>>> {}

    public class GetAllVideoGameFilterQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllVideoGameFilterQuery, ResponseDto<IEnumerable<GetAllVideoGameDto>>>
    {
        #region Parameters
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        #endregion

        public async Task<ResponseDto<IEnumerable<GetAllVideoGameDto>>> Handle(GetAllVideoGameFilterQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Videojuego, bool>> where = c => c.Activo 
                && (string.IsNullOrEmpty(request.Nombre) || c.Nombre == request.Nombre)
                && (!request.IdCompania.HasValue || c.IdCompania == request.IdCompania)
                && (!request.AnioLanzamiento.HasValue || c.AnioLanzamiento == request.AnioLanzamiento);
            var includeConfigurations = new (Expression<Func<Videojuego, object>> Include, Expression<Func<object, object>>? ThenInclude)[]
            {
                (videoJuego => videoJuego.IdCompaniaNavigation, null)
            };
            var pagination = () => (request.PageNumber, request.PageSize);
            var (Data, TotalRecords) = await _unitOfWork.VideojuegoRepository.GetAllAsync(where: where, includeProperties: includeConfigurations, paginationExpr: pagination);
            if (!Data.Any()) throw new NotFoundException("No hay videojuegos en el sistema que cumplan con los filtros ingresados.");
            var mappedData = _mapper.Map<IEnumerable<GetAllVideoGameDto>>(Data);
            var response = new ResponseDto<IEnumerable<GetAllVideoGameDto>>
            {
                Data = mappedData,
                Message = "Éxito",
                TotalRecords = TotalRecords
            };
            
            return response;   
        } 
    }
}

