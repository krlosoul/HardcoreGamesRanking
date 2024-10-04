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

    public class GetVideoGameByIdQuery : GetVideoGameByIdDto, IRequest<ResponseDto<GetAllVideoGameDto>> {}

    public class GetVideoGameByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetVideoGameByIdQuery, ResponseDto<GetAllVideoGameDto>>
    {
        #region Parameters
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        #endregion

        public async Task<ResponseDto<GetAllVideoGameDto>> Handle(GetVideoGameByIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Videojuego, bool>> where = c => c.Activo && c.Id == request.IdVideoGame;
            var includeConfigurations = new (Expression<Func<Videojuego, object>> Include, Expression<Func<object, object>>? ThenInclude)[]
            {
                (videoJuego => videoJuego.IdCompaniaNavigation, null)
            };
            var data = await _unitOfWork.VideojuegoRepository.FirstOrDefaultAsync(where, includeConfigurations) ?? throw new NotFoundException("No hay un videojuego en el sistema con ese id.");
            var mappedData = _mapper.Map<GetAllVideoGameDto>(data);
            var response = new ResponseDto<GetAllVideoGameDto>
            {
                Data = mappedData,
                Message = "Éxito"
            };
            
            return response;   
        } 
    }
}

