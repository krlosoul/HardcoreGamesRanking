namespace HardcoreGamesRanking.Business.Features.User.Queries
{
    using System.Threading;
    using Business.Features.User.Dtos;
    using Core.Dtos;
    using Business.Interfaces.DataAccess;
    using MediatR;
    using Business.Interfaces.Services;
    using Core.Extensions;
    using Business.Common.Exceptions;
    using AutoMapper;
    using HardcoreGamesRanking.Core.Entities;

    public class AuthenticateQuery : AuthenticateDto, IRequest<ResponseDto<TokenDto>> { }

    public class AuthenticateQueryHandler(IUnitOfWork unitOfWork, IJwtService jwtService) : IRequestHandler<AuthenticateQuery, ResponseDto<TokenDto>>
    {
        #region Parameters
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IJwtService _jwtService = jwtService;
        #endregion

        public async Task<ResponseDto<TokenDto>> Handle(AuthenticateQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsuarioRepository.FirstOrDefaultAsync(x => x.Email.Trim().ToLower().Equals(request.Email.Trim().ToLower())) ?? throw new NotFoundException($"Error email or password.");
            if (!user.Password.Trim().DecryptString().Equals(request.Password.Trim())) throw new NotFoundException($"Error email or password.");

            ClaimDto claimDto =new() { IdUsuario = user.Id};

            var token = _jwtService.GenerateToken(claimDto);

            TokenDto tokenDto = new() { Token = token };

            return Response($"Usuario {request.Email} logeado correctamente.", tokenDto);
        }

        #region Private Methods
        private static ResponseDto<TokenDto> Response(string message, TokenDto token)
        {
            return new ResponseDto<TokenDto>()
            {
                Message = message,
                Data = token
            };
        }
        #endregion
    }
}