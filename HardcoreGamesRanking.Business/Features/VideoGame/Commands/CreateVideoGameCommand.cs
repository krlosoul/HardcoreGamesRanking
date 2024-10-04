namespace HardcoreGamesRanking.Business.Features.VideoGame.Commands
{
    using AutoMapper;
    using Business.Features.VideoGame.Dtos;
    using Core.Dtos;
    using Business.Interfaces.DataAccess;
    using MediatR;
    using System.Threading.Tasks;
    using System.Threading;
    using Business.Common.Exceptions;
    using HardcoreGamesRanking.Core.Entities;
    using Microsoft.Extensions.Caching.Memory;

    public class CreateVideoGameCommand : CreateVideoGameDto, IRequest<ResponseDto>{}

    public class CreateVideoGameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache): IRequestHandler<CreateVideoGameCommand, ResponseDto>
    {
        #region Parameters
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IMemoryCache _memoryCache = memoryCache;
        #endregion

        public async Task<ResponseDto> Handle(CreateVideoGameCommand request, CancellationToken cancellationToken)
        {
            await VideoGameExists(request);
            await CompanieExists(request);
            await CreateVideoGame(request);
            _memoryCache.Remove("GetAllVideoGameQuery");
            return Response("Se creo el videojuego correctamente.");
        }

        private async Task VideoGameExists(CreateVideoGameCommand request)
        {
            var exists = await _unitOfWork.VideojuegoRepository.AnyAsync(x => x.Nombre.Equals(request.Nombre) && x.Activo);
            if (exists) throw new BadRequestException($"El videojuego llamado {request.Nombre} ya existe.");
        }

        private async Task CompanieExists(CreateVideoGameCommand request)
        {
            var exists = await _unitOfWork.CompaniaRepository.AnyAsync(x => x.Id.Equals(request.IdCompania));
            if (!exists) throw new BadRequestException($"La compania con identificacion numero {request.IdCompania} no existe.");
        }

        private async Task CreateVideoGame(CreateVideoGameCommand request)
        {
            var videoGame = _mapper.Map<CreateVideoGameDto, Videojuego>(request);
            videoGame.FechaRegistro = DateTime.Now;
            videoGame.Activo = true;
            bool insert = await _unitOfWork.VideojuegoRepository.InsertAsync(videoGame);
            if (!insert) throw new BadRequestException("No se pudo registrar el Videojuego en el sistema.");
        }

        private static ResponseDto Response(string message)
        {
            return new ResponseDto()
            {
                Message = message
            };
        }
    }
}