using DataAcess;
using IdentityManager.Services.ControllerService.IControllerService;
using IdentityManagerAPI.ControllerService.IControllerService;

namespace IdentityManagerAPI.ControllerService
{
    public class DistanceAndRatingStrategy : IDistanceCalculationStrategy
    {
        private readonly IGeolocationService _geoService;

        public DistanceAndRatingStrategy(IGeolocationService geoService)
        {
            _geoService = geoService;
        }

        public double CalculateSorting(Worker worker, double userLat, double userLon)
        {
            double distance = _geoService.CalculateDistance(Convert.ToDouble(worker.Lat), Convert.ToDouble(worker.Long), userLat, userLon);
            double normalizedDistance = 1 / (1 + distance);
            return (normalizedDistance * 0.5) + (worker.Rating * 0.5);
        }
    }
}
