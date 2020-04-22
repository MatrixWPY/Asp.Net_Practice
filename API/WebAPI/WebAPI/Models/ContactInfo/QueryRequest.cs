using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.ContactInfo
{
    public class QueryRequest
    {
        [Required]
        public long? ContactInfoID { get; set; }

        [Required]
        public string Sign { get; set; }
    }
}