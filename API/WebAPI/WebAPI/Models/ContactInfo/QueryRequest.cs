using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.ContactInfo
{
    public class QueryRequest
    {
        [Required(ErrorMessage = "{0} 為必填欄位。")]
        public long? ContactInfoID { get; set; }

        [Required(ErrorMessage = "{0} 為必填欄位。")]
        public string Sign { get; set; }
    }
}