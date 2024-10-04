namespace HardcoreGamesRanking.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Business.Features.Rating.Queries;
    using System.Text;

    [ApiController]
    [Route("api/V1/[controller]")]
    public class RatingController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Get top rating.
        /// </summary>
        /// <returns>File.</returns>
        [Authorize]
        [HttpGet("GetRanking")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetRankingAsync([FromQuery] GetRankingQuery getRankingQuery)
        {
            var result = await _mediator.Send(getRankingQuery);
            var bytes = Encoding.UTF8.GetBytes(result.Data!);
            return base.File(bytes, "text/csv", "ranking_videojuegos.csv");
        }
    }
}

