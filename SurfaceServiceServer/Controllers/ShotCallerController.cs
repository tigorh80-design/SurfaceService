using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Shared.ShotCaller;

namespace SurfaceServiceServer.Controllers
{
    [ApiController]
    [Route("ShotCallerController")]
    public class ShotCallerController : ControllerBase
    {
        private readonly ILogger<ShotCallerController> _logger;
        private IShotCallerService _shotCallerService;

        public ShotCallerController(ILogger<ShotCallerController> logger, IShotCallerService shotCallerService)
        {
            _logger = logger;
            _shotCallerService = shotCallerService;
        }
       
        /// <summary>
        /// Create a new ShotCaller record
        /// </summary>
        /// <param name="forumPostRequest"></param>
        /// <returns></returns>
        [HttpPost("createShotCallerRecord")]
        public async Task CreateShotCallerRecordAsync([FromBody] ShotCallerRequest shotCallerRequest)
        {
            try
            {
                await _shotCallerService.CreateShotCallerRecordAsync(shotCallerRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateShotCallerRecordAsync");
                throw;
            }
        }

    }
}
