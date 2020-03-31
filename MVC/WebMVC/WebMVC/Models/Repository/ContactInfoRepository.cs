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
        public ContactInfoRepository(IDbConnection objConnect = null) : base(objConnect)
        {
        }

        public List<ContactInfoData> GetContactInfo()
        {
            List<ContactInfoData> liContactInfoData = null;

            using (var objConnect = GetDBConnection())
            {
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.AppendLine("select * from Tbl_ContactInfo");
                sbSQL.AppendLine("where IsEnable=1");
                sbSQL.AppendLine("order by ContactInfoID asc");

                liContactInfoData = objConnect.Query<ContactInfoData>(sbSQL.ToString()).ToList();
            }

            return liContactInfoData;
        }

        public ContactInfoData GetContactInfo(long ContactInfoID)
        {
            ContactInfoData objContactInfoData = null;

            using (var objConnect = GetDBConnection())
            {
                StringBuilder sbSQL = new StringBuilder();
                sbSQL.AppendLine("select * from Tbl_ContactInfo");
                sbSQL.AppendLine("where ContactInfoID=@ContactInfoID");
                sbSQL.AppendLine("order by ContactInfoID desc");

                objContactInfoData = objConnect.Query<ContactInfoData>(sbSQL.ToString(), new { ContactInfoID = ContactInfoID }).FirstOrDefault();
            }

            return objContactInfoData;
        }

        public bool AddContactInfo(ContactInfoData objContactInfoData, IDbTransaction objOrgTran = null)
        {
            bool bolResult = false;

            try
            {
                if (null == objOrgTran)
                {
                    using (var objConnect = GetDBConnection())
                    {
                        objContactInfoData.IsEnable = true;
                        objContactInfoData.CreateTime = DateTime.Now;

                        long? ContactInfoID = objConnect.Insert(objContactInfoData);

                        objContactInfoData.ContactInfoID = Convert.ToInt64(ContactInfoID);
                    }
                }
                else
                {
                    objContactInfoData.IsEnable = true;
                    objContactInfoData.CreateTime = DateTime.Now;

                    long? ContactInfoID = GetDBConnection().Insert(objContactInfoData, objOrgTran);

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

        public bool UpdateContactInfo(ContactInfoData objContactInfoData, IDbTransaction objOrgTran = null)
        {
            bool bolResult = false;

            try
            {
                if (null == objOrgTran)
                {
                    using (var objConnect = GetDBConnection())
                    {
                        objContactInfoData.UpdateTime = DateTime.Now;

                        objConnect.Update(objContactInfoData);
                    }
                }
                else
                {
                    objContactInfoData.UpdateTime = DateTime.Now;

                    GetDBConnection().Update(objContactInfoData, objOrgTran);
                }

                bolResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bolResult;
        }

        public bool DeleteContactInfo(ContactInfoData objContactInfoData, IDbTransaction objOrgTran = null)
        {
            bool bolResult = false;

            try
            {
                if (null == objOrgTran)
                {
                    using (var objConnect = GetDBConnection())
                    {
                        objConnect.Delete(objContactInfoData);
                    }
                }
                else
                {
                    GetDBConnection().Delete(objContactInfoData, objOrgTran);
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