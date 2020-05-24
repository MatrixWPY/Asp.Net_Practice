using Dapper;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WcfService.Model;

namespace WcfService.Repository
{
    public class ContactInfoRESTfulRepository
    {
        private static string DBConnectString = ConfigurationManager.ConnectionStrings["DBConnect"].ConnectionString;

        public ContactInfoModel Query(long lContactInfoID)
        {
            ContactInfoModel objContactInfoModel = null;

            try
            {
                using (var db = new SqlConnection(DBConnectString))
                {
                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendLine("SELECT * FROM Tbl_ContactInfo");
                    sbSQL.AppendLine("WHERE ContactInfoID=@ContactInfoID");
                    sbSQL.AppendLine("ORDER BY ContactInfoID DESC");

                    objContactInfoModel = db.Query<ContactInfoModel>(sbSQL.ToString(), new { ContactInfoID = lContactInfoID }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objContactInfoModel;
        }

        public bool Insert(ContactInfoModel objContactInfoModel)
        {
            bool bolResult = false;

            try
            {
                using (var db = new SqlConnection(DBConnectString))
                {
                    objContactInfoModel.IsEnable = true;
                    objContactInfoModel.CreateTime = DateTime.Now;

                    long? lContactInfoID = db.Insert<ContactInfoModel>(objContactInfoModel);

                    objContactInfoModel.ContactInfoID = Convert.ToInt64(lContactInfoID);
                }
                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool Update(ContactInfoModel objContactInfoModel)
        {
            bool bolResult = false;

            try
            {
                using (var db = new SqlConnection(DBConnectString))
                {
                    objContactInfoModel.UpdateTime = DateTime.Now;

                    db.Update<ContactInfoModel>(objContactInfoModel);
                }
                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool Delete(ContactInfoModel objContactInfoModel)
        {
            bool bolResult = false;

            try
            {
                using (var db = new SqlConnection(DBConnectString))
                {
                    db.Delete<ContactInfoModel>(objContactInfoModel);
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