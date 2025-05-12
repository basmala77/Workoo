namespace IdentityManagerAPI.Controllers
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        public string ClientId { get; set; }  
        public string ClientName { get; set; }
        public string ServiceType { get; set; }
        public string Status { get; set; } = "Pending"; // (Pending, Accepted, Completed)
    }
}