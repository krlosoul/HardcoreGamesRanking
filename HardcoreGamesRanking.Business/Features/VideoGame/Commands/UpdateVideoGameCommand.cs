namespace HardcoreGamesRanking.Business.Features.VideoGame.Commands
{
    using Business.Features.VideoGame.Dtos;
    using Core.Dtos;
    using Business.Interfaces.DataAccess;
    using MediatR;
    using System.Threading.Tasks;
    using System.Threading;
    using Business.Common.Exceptions;
    using HardcoreGamesRanking.Core.Entities;
    using Business.Interfaces.Services;
    using Microsoft.Extensions.Caching.Memory;

    public class UpdateVideoGameCommand : UpdateVideoGameDto, IRequest<ResponseDto>{}

    public class UpdateVideoGameCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMemoryCache memoryCache): IRequestHandler<UpdateVideoGameCommand, ResponseDto>
    {
        #region Parameters
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMemoryCache _memoryCache = memoryCache;
        #endregion

        public async Task<ResponseDto> Handle(UpdateVideoGameCommand request, CancellationToken cancellationToken)
        {
            var videoGame = await GetVideoGame(request);
            await VideoGameExists(request);
            await CompanieExists(request);
            await UpdateVideoGame(videoGame);
            _memoryCache.Remove("GetAllVideoGameQuery");
            return Response("Se actualizo el videojuego correctamente.");
        }

        private async Task<Videojuego> GetVideoGame(UpdateVideoGameCommand request)
        {
            var videoGame = await _unitOfWork.VideojuegoRepository.FirstOrDefaultAsync(x => x.Id.Equals(request.IdVideoGame) && x.Activo) ?? throw new BadRequestException($"El videojuego no existe.");
            return videoGame;
        }

        private async Task VideoGameExists(UpdateVideoGameCommand request)
        {
            var exists = await _unitOfWork.VideojuegoRepository.AnyAsync(x => x.Nombre.Equals(request.Nombre) && x.Activo);
            if (exists) throw new BadRequestException($"El videojuego llamado {request.Nombre} ya existe.");
        }

        private async Task CompanieExists(UpdateVideoGameCommand request)
        {
            var exists = await _unitOfWork.CompaniaRepository.AnyAsync(x => x.Id.Equals(request.IdCompania));
            if (!exists) throw new BadRequestException($"La compania con identificacion numero {request.IdCompania} no existe.");
        }

        private async Task UpdateVideoGame(Videojuego videojuego)
        {
            var userId = _currentUserService.GetCurrentIdUser();
            videojuego.FechaActualizacion = DateTime.Now;
            videojuego.UsuarioActualizacion = userId;
            bool update = await _unitOfWork.VideojuegoRepository.UpdateAsync(videojuego);
            if (!update) throw new BadRequestException("No se pudo actualizar el Videojuego en el sistema.");
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