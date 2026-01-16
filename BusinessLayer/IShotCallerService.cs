using Shared.ShotCaller;

namespace BusinessLayer
{
    public interface IShotCallerService
    {
        Task CreateShotCallerRecordAsync(ShotCallerRequest shotCallerRequest);
        Task<List<ShotCallerResponse>> GetAllShotCallerRecordsAsync();
    }
}