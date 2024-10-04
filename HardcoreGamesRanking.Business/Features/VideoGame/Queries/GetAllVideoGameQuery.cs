namespace HardcoreGamesRanking.Business.Features.VideoGame.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Business.Features.VideoGame.Dtos;
    using Core.Dtos;
    using Business.Interfaces.DataAccess;
    using MediatR;
    using Microsoft.Extensions.Caching.Memory;
    using Business.Common.Exceptions;
    using Core.Entities;
    using System.Linq.Expressions;

    public class GetAllVideoGameQuery: IRequest<ResponseDto<IEnumerable<GetAllVideoGameDto>>> {}

    public class GetAllVideoGameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache) : IRequestHandler<GetAllVideoGameQuery, ResponseDto<IEnumerable<GetAllVideoGameDto>>>
    {
        #region Parameters
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IMemoryCache _memoryCache = memoryCache;
        #endregion

        public async Task<ResponseDto<IEnumerable<GetAllVideoGameDto>>> Handle(GetAllVideoGameQuery request, CancellationToken cancellationToken)
        {
            const string cacheKey = "GetAllVideoGameQuery";
            var cachedResponse =  GetFromCacheAsync(cacheKey);
            if (cachedResponse == null) {
                cachedResponse = await GetFromDatabaseAsync();
                SetFromCacheAsync(cacheKey, cachedResponse);
            }
            return cachedResponse;
        }

        private ResponseDto<IEnumerable<GetAllVideoGameDto>>? GetFromCacheAsync(string cacheKey)
        {
            return _memoryCache.Get<ResponseDto<IEnumerable<GetAllVideoGameDto>>?>(cacheKey);
        }

        private  async Task<ResponseDto<IEnumerable<GetAllVideoGameDto>>> GetFromDatabaseAsync()
        {
            Expression<Func<Videojuego, bool>> where = c => c.Activo;
            var includeConfigurations = new (Expression<Func<Videojuego, object>> Include, Expression<Func<object, object>>? ThenInclude)[]
            {
                (videoJuego => videoJuego.IdCompaniaNavigation, null)
            };
            var (Data, TotalRecords) = await _unitOfWork.VideojuegoRepository.GetAllAsync(where: where, includeProperties: includeConfigurations);
            if (!Data.Any()) throw new NotFoundException("No hay videojuegos en el sistema.");
            var mappedData = _mapper.Map<IEnumerable<GetAllVideoGameDto>>(Data);
            var response = new ResponseDto<IEnumerable<GetAllVideoGameDto>>
            {
                Data = mappedData,
                Message = "Éxito",
                TotalRecords = TotalRecords
            };
            
            return response;
        }

        private  void SetFromCacheAsync(string cacheKey, ResponseDto<IEnumerable<GetAllVideoGameDto>> data)
        {
            _memoryCache.Set(cacheKey, data);
        }
    }
}

