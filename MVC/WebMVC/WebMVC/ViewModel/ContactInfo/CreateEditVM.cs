using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebMVC.ViewModel.ContactInfo
{
    public class CreateEditVM
    {
        [Key]
        public long ContactInfoID { get; set; }

        [Required(ErrorMessage = "{0} 為必填欄位。")]
        [MaxLength(10, ErrorMessage = "{0} 限定最大長度為10。")]
        public string Name { get; set; }

        [MaxLength(10, ErrorMessage = "{0} 限定最大長度為10。")]
        public string Nickname { get; set; }

        [RegularExpression("Male|Female", ErrorMessage = "{0} 限定為Male或Female。")]
        public EnumGender? Gender { get; set; }

        [RegularExpression("[0-9]{1,3}", ErrorMessage = "{0} 限定為1-3個數字。")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "{0} 為必填欄位。")]
        [RegularExpression("[0-9]{6,20}", ErrorMessage = "{0} 限定為6-20個數字。")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "{0} 為必填欄位。")]
        [MaxLength(100, ErrorMessage = "{0} 限定最大長度為100。")]
        public string Address { get; set; }

        [Required]
        public bool IsEnable { get; set; }

        public enum EnumGender
        {
            [EnumMember]
            Female = 0,

            [EnumMember]
            Male = 1
        }
    }
}