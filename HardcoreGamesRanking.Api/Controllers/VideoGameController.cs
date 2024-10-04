namespace HardcoreGamesRanking.Api.Controllers
{
    using Core.Dtos;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Business.Features.VideoGame.Queries;
    using Business.Features.VideoGame.Dtos;
    using Business.Features.VideoGame.Commands;

    [ApiController]
    [Route("api/V1/[controller]")]
    public class VideoGameController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Get All VideoGames.
        /// </summary>
        /// <returns>IEnumerable&lt;VideoGames&gt;</returns>
        [Authorize]
        [HttpGet("GetAllVideoGames")]
        [Consumes("application/json")]
        public Task<ResponseDto<IEnumerable<GetAllVideoGameDto>>> GetAllVideoGamesAsync() => _mediator.Send(new GetAllVideoGameQuery());

        /// <summary>
        /// Get All VideoGames by filter.
        /// </summary>
        /// <param name="getAllVideoGameFilterQuery">The parameters.</param>
        /// <returns>IEnumerable&lt;VideoGames&gt;</returns>
        [HttpGet("GetAllVideoGamesFilter")]
        [Consumes("application/json")]
        [Authorize]
        public Task<ResponseDto<IEnumerable<GetAllVideoGameDto>>> GetAllVideoGamesFilterAsync([FromQuery] GetAllVideoGameFilterQuery getAllVideoGameFilterQuery) => _mediator.Send(getAllVideoGameFilterQuery);

        /// <summary>
        /// Get VideoGame by id.
        /// </summary>
        /// <param name="getVideoGameByIdQuery">The parameters.</param>
        /// <returns>VideoGame</returns>
        [HttpGet("GetVideoGameById/{IdVideoGame}")]
        [Consumes("application/json")]
        [Authorize]
        public Task<ResponseDto<GetAllVideoGameDto>> GetVideoGameByIdAsync([FromRoute] GetVideoGameByIdQuery getVideoGameByIdQuery) => _mediator.Send(getVideoGameByIdQuery);

        /// <summary>
        /// Create a VideoGame.
        /// </summary>
        /// <param name="createVideoGameCommand">The parameters.</param>
        /// <returns>VideoGame</returns>
        [HttpPost("CreateVideoGame")]
        [Consumes("application/json")]
        [Authorize]
        public Task<ResponseDto> CreateVideoGameAsync([FromBody] CreateVideoGameCommand createVideoGameCommand) => _mediator.Send(createVideoGameCommand);

        /// <summary>
        /// Update a VideoGame.
        /// </summary>
        /// <param name="updateVideoGameCommand">The parameters.</param>
        /// <returns>VideoGame</returns>
        [HttpPut("UpdateVideoGame")]
        [Consumes("application/json")]
        [Authorize]
        public Task<ResponseDto> UpdateVideoGameAsync([FromBody] UpdateVideoGameCommand updateVideoGameCommand) => _mediator.Send(updateVideoGameCommand);

        /// <summary>
        /// Delete a VideoGame.
        /// </summary>
        /// <param name="deleteVideoGameCommand">The parameters.</param>
        /// <returns>VideoGame</returns>
        [HttpDelete("DeleteVideoGame/{IdVideoGame}")]
        [Consumes("application/json")]
        [Authorize]
        public Task<ResponseDto> DeleteVideoGameAsync([FromRoute] DeleteVideoGameCommand deleteVideoGameCommand) => _mediator.Send(deleteVideoGameCommand);

    }
}

