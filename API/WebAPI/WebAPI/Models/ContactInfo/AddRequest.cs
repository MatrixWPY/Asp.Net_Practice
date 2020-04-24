using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.ContactInfo
{
    public class AddRequest
    {
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Nickname { get; set; }

        [Range(0, 1, ErrorMessage = "{0} 限定為0或1。")]
        public int? Gender { get; set; }

        [RegularExpression("[0-9]{0,127}", ErrorMessage = "{0} 限定為0-127個數字。")]
        public int? Age { get; set; }

        [Required]
        [RegularExpression("[0-9]{6,20}", ErrorMessage = "{0} 限定為6-20個數字。")]
        [MaxLength(20)]
        public string PhoneNo { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        public string Sign { get; set; }
    }
}