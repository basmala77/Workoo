using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityManagerAPI.Domain
{
    public class Worker
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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