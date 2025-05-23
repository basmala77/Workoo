﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Services.ControllerService.IControllerService
{
    public interface IGeolocationService
    {
        Task<string> GetAddressFromCoordinates(double latitude, double longitude);
    }
}
