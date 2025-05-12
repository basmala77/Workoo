using IdentityManager.Services.ControllerService.IControllerService;
using IdentityManagerAPI.ControllerService.IControllerService;

namespace IdentityManagerAPI.ControllerService
{
    public class DistanceStrategyFactory
    {
        private readonly IGeolocationService _geoService;

        public DistanceStrategyFactory(IGeolocationService geoService)
        {
            _geoService = geoService;
        }

        public IDistanceCalculationStrategy GetStrategy(string sortBy)
        {
            return sortBy switch
            {
                "distance" => new GeolocationDistanceStrategy(_geoService),
                "rating" => new DistanceAndRatingStrategy(_geoService),
                _ => new GeolocationDistanceStrategy(_geoService)
            };
        }
    }
}
