using System.Text.Json;
using IdentityManager.Services.ControllerService.IControllerService;

namespace IdentityManagerAPI.ControllerService
{
    ///////////Adapter pattern ////////////////
    public class OpenStreetMapAdapter : IGeolocationService
    {
        private readonly HttpClient _httpClient;

        public OpenStreetMapAdapter(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "IdentityManagerAPI/1.0 (basmalaelshabrawy4@gmail.com)");
        }

        public async Task<string?> GetAddressFromCoordinates(double latitude, double longitude)
        {
            string url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={latitude}&lon={longitude}";
            var response = await _httpClient.GetStringAsync(url);
            var json = JsonDocument.Parse(response);
            return json.RootElement.GetProperty("display_name").GetString();
        }
        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double r = 6371; 
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return r * c; 
        }

        private double ToRadians(double degrees) => degrees * (Math.PI / 180);
    
    }
}

