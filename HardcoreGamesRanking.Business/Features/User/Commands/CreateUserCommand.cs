namespace HardcoreGamesRanking.Business.Features.User.Commands
{
    using Business.Features.User.Dtos;
    using Core.Dtos;
    using Business.Interfaces.DataAccess;
    using MediatR;
    using AutoMapper;
    using System.Threading.Tasks;
    using System.Threading;
    using Business.Common.Exceptions;
    using HardcoreGamesRanking.Core.Entities;

    public class CreateUserCommand: CreateUserDto, IRequest<ResponseDto>{}

    public class CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateUserCommand, ResponseDto>
    {
        #region Parameters
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        #endregion

        public async Task<ResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await UserExists(request);
            int userId = await CreateUser(request);

            return Response($"Se creo el usuario con el id {userId} correctamente.");
        }

        #region Private Methods
        private async Task UserExists(CreateUserCommand request)
        {
            var existsEmail = await _unitOfWork.UsuarioRepository.AnyAsync(x => x.Email.Equals(request.Email));
            if (existsEmail) throw new BadRequestException($"El correo {request.Email} ya existe.");
        }

        private async Task<int> CreateUser(CreateUserCommand request)
        {
            var user = _mapper.Map<Usuario>(request);
            user.FechaRegistro = DateTime.Now;
            bool insert = await _unitOfWork.UsuarioRepository.InsertAsync(user);
            if (!insert) throw new BadRequestException("No se pudo registrar el usuario en el sistema.");
            return user.Id;
        }
        
        private static ResponseDto Response(string message)
        {
            return new ResponseDto()
            {
                Message = message
            };
        }
        #endregion
    }
}