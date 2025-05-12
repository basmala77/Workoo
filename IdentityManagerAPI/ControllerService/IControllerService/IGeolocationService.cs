using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Services.ControllerService.IControllerService
{
    /////////////Adapter Pattarn /////////////////////
    public interface IGeolocationService
    {
        Task<string?> GetAddressFromCoordinates(double latitude, double longitude);
        double CalculateDistance(double lat1, double lon1, double lat2, double lon2);
    }
}
