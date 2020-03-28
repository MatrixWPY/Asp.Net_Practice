using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebMVC.Models.Data;

namespace WebMVC.Models.Repository
{
    public class ContactInfoRepository
    {
        private static readonly ConnectionStringSettings ConnectionSet = ConfigurationManager.ConnectionStrings["DBConnect"];

        public List<ContactInfoData> GetContactInfo()
        {
            List<ContactInfoData> liContactInfoData = null;

            using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
            {
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.AppendLine("select * from Tbl_ContactInfo");
                sbSQL.AppendLine("where IsEnable=1");
                sbSQL.AppendLine("order by ContactInfoID desc");

                liContactInfoData = cn.Query<ContactInfoData>(sbSQL.ToString()).ToList();
            }

            return liContactInfoData;
        }

        public ContactInfoData GetContactInfo(long ContactInfoID)
        {
            ContactInfoData objContactInfoData = null;

            using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
            {
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.AppendLine("select * from Tbl_ContactInfo");
                sbSQL.AppendLine("where ContactInfoID=@ContactInfoID");
                sbSQL.AppendLine("order by ContactInfoID desc");

                objContactInfoData = cn.Query<ContactInfoData>(sbSQL.ToString(), new { ContactInfoID = ContactInfoID }).FirstOrDefault();
            }

            return objContactInfoData;
        }

        public bool AddContactInfo(ContactInfoData objContactInfoData)
        {
            bool bolResult = false;

            try
            {
                using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
                {
                    objContactInfoData.IsEnable = true;
                    objContactInfoData.CreateTime = DateTime.Now;

                    long? ContactInfoID = cn.Insert(objContactInfoData);

                    objContactInfoData.ContactInfoID = Convert.ToInt64(ContactInfoID);
                }

                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool UpdateContactInfo(ContactInfoData objContactInfoData)
        {
            bool bolResult = false;

            try
            {
                using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
                {
                    objContactInfoData.UpdateTime = DateTime.Now;

                    cn.Update(objContactInfoData);
                }

                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool DeleteContactInfo(ContactInfoData objContactInfoData)
        {
            bool bolResult = false;

            try
            {
                using (var cn = new SqlConnection(ConnectionSet.ConnectionString))
                {
                    cn.Delete(objContactInfoData);
                }

                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }
    }
}