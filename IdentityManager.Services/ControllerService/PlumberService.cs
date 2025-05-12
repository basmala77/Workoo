using IdentityManager.Services.ControllerService.IControllerService;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Services.ControllerService
{
    public class PlumberService : IService
    {
        public string GetServiceType() => "Plumber";
    }
    public class ElectricianService : IService
    {
        public string GetServiceType() => "Electrician";
    }
}
