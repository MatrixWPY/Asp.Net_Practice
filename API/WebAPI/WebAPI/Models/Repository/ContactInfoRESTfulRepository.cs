using Dapper;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPI.Models.Data;

namespace WebAPI.Models.Repository
{
    public class ContactInfoRESTfulRepository
    {
        private static string DBConnectString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;

        public ContactInfoData Query(long lContactInfoID)
        {
            ContactInfoData objContactInfoData = null;

            try
            {
                using (var db = new SqlConnection(DBConnectString))
                {
                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendLine("SELECT * FROM Tbl_ContactInfo");
                    sbSQL.AppendLine("WHERE ContactInfoID=@ContactInfoID");
                    sbSQL.AppendLine("ORDER BY ContactInfoID DESC");

                    objContactInfoData = db.Query<ContactInfoData>(sbSQL.ToString(), new { ContactInfoID = lContactInfoID }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objContactInfoData;
        }

        public bool Insert(ContactInfoData objContactInfoData)
        {
            bool bolResult = false;

            try
            {
                using (var db = new SqlConnection(DBConnectString))
                {
                    objContactInfoData.IsEnable = true;
                    objContactInfoData.CreateTime = DateTime.Now;

                    long? lContactInfoID = db.Insert<ContactInfoData>(objContactInfoData);

                    objContactInfoData.ContactInfoID = Convert.ToInt64(lContactInfoID);
                }
                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool Update(ContactInfoData objContactInfoData)
        {
            bool bolResult = false;

            try
            {
                using (var db = new SqlConnection(DBConnectString))
                {
                    objContactInfoData.UpdateTime = DateTime.Now;

                    db.Update<ContactInfoData>(objContactInfoData);
                }
                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool Delete(ContactInfoData objContactInfoData)
        {
            bool bolResult = false;

            try
            {
                using (var db = new SqlConnection(DBConnectString))
                {
                    db.Delete<ContactInfoData>(objContactInfoData);
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