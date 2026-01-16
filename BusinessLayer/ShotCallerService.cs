using RepositoryLayer.Repositories;
using Shared.ShotCaller;

namespace BusinessLayer
{
    public class ShotCallerService : IShotCallerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IShotCallerRepository _shotCallerRepository;

        public ShotCallerService(IHttpClientFactory httpClientFactory, IShotCallerRepository shotCallerRepository)
        {
            _httpClientFactory = httpClientFactory;
            _shotCallerRepository = shotCallerRepository;
        }

        public async Task CreateShotCallerRecordAsync(ShotCallerRequest shotCallerRequest)
        {
            await _shotCallerRepository.AddAsync(shotCallerRequest);
        }

        public async Task<List<ShotCallerResponse>> GetAllShotCallerRecordsAsync()
        { 
            var shotCallerRecords = await _shotCallerRepository.GetAllAsync();
            return shotCallerRecords.Select(entity => new ShotCallerResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Tequila = entity.Tequila,
                Vodka = entity.Vodka,
                Beers = entity.Beers,
                CreatedDate = entity.CreatedDate
            }).ToList();
        }
    }
}
