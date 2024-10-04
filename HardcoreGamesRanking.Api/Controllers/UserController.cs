namespace HardcoreGamesRanking.Api.Controllers
{
    using Business.Features.User.Commands;
    using Core.Dtos;
    using Business.Features.User.Dtos;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Business.Features.User.Queries;

    [ApiController]
    [Route("api/V1/[controller]")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="createUserCommand">The parameters.</param>
        /// <returns>Response.</returns>
        [HttpPost("CreateUser")]
        [Consumes("application/json")]
        [AllowAnonymous]
        public async Task<ResponseDto> CreateUserAsync([FromBody] CreateUserCommand createUserCommand) => await _mediator.Send(createUserCommand);

        /// <summary>
        /// User authentication.
        /// </summary>
        /// <param name="authenticateQuery">The parameters.</param>
        /// <returns>Token.</returns>
        [HttpPost("Authenticate")]
        [Consumes("application/json")]
        [AllowAnonymous]
        public Task<ResponseDto<TokenDto>> AuthenticateAsync([FromBody] AuthenticateQuery authenticateQuery) => _mediator.Send(authenticateQuery);
    }
}

