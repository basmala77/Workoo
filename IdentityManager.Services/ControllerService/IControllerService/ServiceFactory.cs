using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Services.ControllerService.IControllerService
{
    public class ServiceFactory
    {
        public static IService CreateService(string serviceType)
        {
            return serviceType.ToLower() switch
            {
                "plumber" => new PlumberService(),
                "electrician" => new ElectricianService(),
                _ => throw new ArgumentException("Unsupported service! ")
            };
        }

       
    }
}
