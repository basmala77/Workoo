
using DataAcess;

namespace IdentityManagerAPI.ControllerService.IControllerService
{
    /////////////Strategy Pattern//////////////
    public interface IDistanceCalculationStrategy
    {
        double CalculateSorting(Worker worker, double userLat, double userLon);
    }
}
