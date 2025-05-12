using AutoMapper.Configuration.Annotations;

namespace DataAcess
{
    public class Worker
    {
        [Ignore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Specialty { get; set; }
        public string Location { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public double Rating { get; set; }
        public double ExperienceYears { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
    }
}