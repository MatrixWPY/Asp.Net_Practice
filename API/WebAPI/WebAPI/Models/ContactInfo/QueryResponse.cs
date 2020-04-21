using System.ComponentModel.DataAnnotations;
using WebMVC.Models.Data;

namespace WebAPI.Models.ContactInfo
{
    public class QueryResponse
    {
        [Required]
        public string Result { get; set; }

        public ContactInfoData Data { get; set; }
    }
}