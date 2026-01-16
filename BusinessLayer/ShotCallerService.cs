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
    }
}
