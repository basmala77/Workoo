using IdentityManager.Services.ControllerService.IControllerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IdentityManager.Services.ControllerService
{
    public class OpenStreetMapAdapter : IGeolocationService
    {
        private readonly HttpClient _httpClient;

        public OpenStreetMapAdapter(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "IdentityManagerAPI/1.0 (basmalaelshabrawy4@gmail.com)");
        }

        public async Task<string> GetAddressFromCoordinates(double latitude, double longitude)
        {
            string url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={latitude}&lon={longitude}";
            var response = await _httpClient.GetStringAsync(url);
            var json = JsonDocument.Parse(response);
            return json.RootElement.GetProperty("display_name").GetString();
        }
    }
}
