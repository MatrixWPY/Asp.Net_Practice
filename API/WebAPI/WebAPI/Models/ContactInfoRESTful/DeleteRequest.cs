using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.ContactInfoRESTful
{
    public class DeleteRequest
    {
        [Required(ErrorMessage = "{0} 為必填欄位。")]
        public long? ContactInfoID { get; set; }
    }
}