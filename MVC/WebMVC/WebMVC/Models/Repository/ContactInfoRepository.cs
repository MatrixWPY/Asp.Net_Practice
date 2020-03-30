using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WebMVC.Models.Data;

namespace WebMVC.Models.Repository
{
    public class ContactInfoRepository : DapperBaseRepository
    {
        public ContactInfoRepository(IDbConnection IConnection = null) : base(IConnection)
        {
        }

        public List<ContactInfoData> GetContactInfo()
        {
            List<ContactInfoData> liContactInfoData = null;

            using (var db = GetDBConnection())
            {
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.AppendLine("select * from Tbl_ContactInfo");
                sbSQL.AppendLine("where IsEnable=1");
                sbSQL.AppendLine("order by ContactInfoID asc");

                liContactInfoData = db.Query<ContactInfoData>(sbSQL.ToString()).ToList();
            }

            return liContactInfoData;
        }

        public ContactInfoData GetContactInfo(long ContactInfoID)
        {
            ContactInfoData objContactInfoData = null;

            using (var db = GetDBConnection())
            {
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.AppendLine("select * from Tbl_ContactInfo");
                sbSQL.AppendLine("where ContactInfoID=@ContactInfoID");
                sbSQL.AppendLine("order by ContactInfoID desc");

                objContactInfoData = db.Query<ContactInfoData>(sbSQL.ToString(), new { ContactInfoID = ContactInfoID }).FirstOrDefault();
            }

            return objContactInfoData;
        }

        public bool AddContactInfo(ContactInfoData objContactInfoData, IDbTransaction ITransaction = null)
        {
            bool bolResult = false;

            try
            {
                if (null == ITransaction)
                {
                    using (var db = GetDBConnection())
                    {
                        objContactInfoData.IsEnable = true;
                        objContactInfoData.CreateTime = DateTime.Now;

                        long? ContactInfoID = db.Insert(objContactInfoData);

                        objContactInfoData.ContactInfoID = Convert.ToInt64(ContactInfoID);
                    }
                }
                else
                {
                    objContactInfoData.IsEnable = true;
                    objContactInfoData.CreateTime = DateTime.Now;

                    long? ContactInfoID = GetDBConnection().Insert(objContactInfoData, ITransaction);

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

        public bool UpdateContactInfo(ContactInfoData objContactInfoData, IDbTransaction ITransaction = null)
        {
            bool bolResult = false;

            try
            {
                if (null == ITransaction)
                {
                    using (var db = GetDBConnection())
                    {
                        objContactInfoData.UpdateTime = DateTime.Now;

                        db.Update(objContactInfoData);
                    }
                }
                else
                {
                    objContactInfoData.UpdateTime = DateTime.Now;

                    GetDBConnection().Update(objContactInfoData, ITransaction);
                }

                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool DeleteContactInfo(ContactInfoData objContactInfoData, IDbTransaction ITransaction = null)
        {
            bool bolResult = false;

            try
            {
                if (null == ITransaction)
                {
                    using (var db = GetDBConnection())
                    {
                        db.Delete(objContactInfoData);
                    }
                }
                else
                {
                    GetDBConnection().Delete(objContactInfoData, ITransaction);
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