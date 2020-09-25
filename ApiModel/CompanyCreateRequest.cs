using System.ComponentModel.DataAnnotations;

namespace GalaxisProjectWebAPI.ApiModel
{
    public class CompanyCreateRequest
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string CompanyAddress { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public string vatNumber { get; set; }

        [Required]
        public string OfficialEmailAddress { get; set; }

        [Required]
        public string ContactPersonName { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string ContactPersonPhoneNumber { get; set; }

        [Required]
        public string ContactPersonEmail { get; set; }
    }
}