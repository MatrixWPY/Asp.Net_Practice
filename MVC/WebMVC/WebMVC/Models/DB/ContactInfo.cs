﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebMVC.Models.DB
{
    [Table("Tbl_ContactInfo")]
    public class ContactInfo
    {
        [Key]
        [DataMember]
        public long ContactInfoID { get; set; }

        [Required]
        [DataMember]
        [MaxLength(10)]
        public string Name { get; set; }

        [DataMember]
        [MaxLength(10)]
        public string Nickname { get; set; }

        [DataMember]
        public EnumGender? Gender { get; set; }

        [DataMember]
        public int? Age { get; set; }

        [Required]
        [DataMember]
        [RegularExpression("[0-9]{6,20}", ErrorMessage = "限定為6-20個數字")]
        [MaxLength(20)]
        public string PhoneNo { get; set; }

        [Required]
        [DataMember]
        [MaxLength(100)]
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
            Male = 0,

            [EnumMember]
            Female = 1
        }
    }
}