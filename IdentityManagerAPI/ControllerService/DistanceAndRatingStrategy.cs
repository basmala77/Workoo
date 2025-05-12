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
        // long method code smell
        // public double CalculateSorting(Worker worker, double userLat, double userLon)
        // {
        //     double distance = _geoService.CalculateDistance(Convert.ToDouble(worker.Lat), Convert.ToDouble(worker.Long), userLat, userLon);
        //     double normalizedDistance = 1 / (1 + distance);
        //     return (normalizedDistance * 0.5) + (worker.Rating * 0.5);
        // }

        public double CalculateSorting(Worker worker, double userLat, double userLon)
        {
            var distance = CalculateDistanceFromUser(worker, userLat, userLon);
            var normalizedDistance = NormalizeDistance(distance);
            return CombineDistanceAndRating(normalizedDistance, worker.Rating);
        }

        private double CalculateDistanceFromUser(Worker worker, double userLat, double userLon)
        {
            double lat = Convert.ToDouble(worker.Lat);
            double lon = Convert.ToDouble(worker.Long);
            return _geoService.CalculateDistance(lat, lon, userLat, userLon);
        }

        private double NormalizeDistance(double distance)
        {
            return 1 / (1 + distance);
        }

        private double CombineDistanceAndRating(double normalizedDistance, double rating)
        {
            return (normalizedDistance * 0.5) + (rating * 0.5);
        }
    }
}