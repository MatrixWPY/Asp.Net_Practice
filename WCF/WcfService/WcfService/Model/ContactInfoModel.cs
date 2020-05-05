using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WcfService.Model
{
    [Table("Tbl_ContactInfo")]
    public class ContactInfoModel
    {
        [Key]
        [DataMember]
        public long ContactInfoID { get; set; }

        [Required]
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Nickname { get; set; }

        [DataMember]
        public EnumGender? Gender { get; set; }

        [DataMember]
        public int? Age { get; set; }

        [Required]
        [DataMember]
        public string PhoneNo { get; set; }

        [Required]
        [DataMember]
        public string Address { get; set; }

        [Required]
        [DataMember]
        public bool IsEnable { get; set; }

        [Required]
        [DataMember]
        [Dapper.IgnoreUpdate]
        public DateTime CreateTime { get; set; }

        [DataMember]
        [Dapper.IgnoreInsert]
        public DateTime? UpdateTime { get; set; }

        public enum EnumGender
        {
            [EnumMember]
            Female = 0,

            [EnumMember]
            Male = 1
        }
    }
}