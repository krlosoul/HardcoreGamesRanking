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

    public class DeleteVideoGameCommand : DeleteVideoGameDto, IRequest<ResponseDto>{}

    public class DeleteVideoGameCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMemoryCache memoryCache): IRequestHandler<DeleteVideoGameCommand, ResponseDto>
    {
        #region Parameters
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMemoryCache _memoryCache = memoryCache;
        #endregion

        public async Task<ResponseDto> Handle(DeleteVideoGameCommand request, CancellationToken cancellationToken)
        {
            var videoGame = await GetVideoGame(request);
            await UpdateVideoGame(videoGame);
            _memoryCache.Remove("GetAllVideoGameQuery");
            return Response("Se elimino el videojuego correctamente.");
        }

        private async Task<Videojuego> GetVideoGame(DeleteVideoGameCommand request)
        {
            var videoGame = await _unitOfWork.VideojuegoRepository.FirstOrDefaultAsync(x => x.Id.Equals(request.IdVideoGame) && x.Activo) ?? throw new BadRequestException($"El videojuego no existe.");
            return videoGame;
        }

        private async Task UpdateVideoGame(Videojuego videojuego)
        {
            var userId = _currentUserService.GetCurrentIdUser();
            videojuego.FechaActualizacion = DateTime.Now;
            videojuego.UsuarioActualizacion = userId;
            videojuego.Activo = false;
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