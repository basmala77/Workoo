using DataAcess;
using IdentityManager.Services.ControllerService.IControllerService;
using IdentityManagerAPI.ControllerService.IControllerService;
namespace IdentityManagerAPI.ControllerService
{
    public class GeolocationDistanceStrategy : IDistanceCalculationStrategy
    {
        private readonly IGeolocationService _geolocationService;
        public GeolocationDistanceStrategy(IGeolocationService geolocationService)
        {
            _geolocationService = geolocationService;
        }
        public double CalculateSorting(Worker worker, double userLat, double userLon)
        {
            double distance = _geolocationService.CalculateDistance(Convert.ToDouble(worker.Lat), Convert.ToDouble(worker.Long), userLat, userLon);
            return 1 / (1 + distance);
        }
    }
}
