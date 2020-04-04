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

        public List<ContactInfoExData> GetContactInfoByCondition(DataTableQueryData objDataTableQueryData)
        {
            List<ContactInfoExData> liContactInfoExData = null;

            try
            {
                using (var objConnect = GetDBConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendLine("SELECT *, (SELECT COUNT(1) FROM Tbl_ContactInfo) AS TotalCount FROM Tbl_ContactInfo");
                    sbSQL.AppendLine("WHERE 1=1");

                    if (objDataTableQueryData.QueryParam.Any())
                    {
                        if (objDataTableQueryData.QueryParam.ContainsKey("IsEnable"))
                        {
                            sbSQL.AppendLine("AND IsEnable=@IsEnable");
                        }
                        if (objDataTableQueryData.QueryParam.ContainsKey("Name"))
                        {
                            sbSQL.AppendLine("AND Name LIKE @Name");
                        }
                        if (objDataTableQueryData.QueryParam.ContainsKey("Nickname"))
                        {
                            sbSQL.AppendLine("AND Nickname LIKE @Nickname");
                        }
                    }

                    string strSort = objDataTableQueryData.DataTableParam.OrderColumn + " " + objDataTableQueryData.DataTableParam.OrderDir;
                    if (!string.IsNullOrWhiteSpace(strSort))
                    {
                        sbSQL.AppendLine("ORDER BY");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Name ASC' THEN Name END ASC,");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Name DESC' THEN Name END DESC,");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Nickname ASC' THEN Nickname END ASC,");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Nickname DESC' THEN Nickname END DESC,");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Gender ASC' THEN Gender END ASC,");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Gender DESC' THEN Gender END DESC,");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Age ASC' THEN Age END ASC,");
                        sbSQL.AppendLine("CASE WHEN @Sort = 'Age DESC' THEN Age END DESC");
                    }

                    sbSQL.AppendLine("OFFSET @Start ROWS FETCH NEXT @Length ROWS ONLY");

                    liContactInfoExData = objConnect.Query<ContactInfoExData>(sbSQL.ToString(), new {
                        IsEnable = (objDataTableQueryData.QueryParam.ContainsKey("IsEnable") ? objDataTableQueryData.QueryParam["IsEnable"] : string.Empty),
                        Name = (objDataTableQueryData.QueryParam.ContainsKey("Name") ? "%" + objDataTableQueryData.QueryParam["Name"] + "%" : string.Empty),
                        Nickname = (objDataTableQueryData.QueryParam.ContainsKey("Nickname") ? "%" + objDataTableQueryData.QueryParam["Nickname"] + "%" : string.Empty),
                        Sort = strSort,
                        Start = objDataTableQueryData.DataTableParam.PageStartRow,
                        Length = objDataTableQueryData.DataTableParam.PageRowCnt
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return liContactInfoExData;
        }

        public ContactInfoData GetContactInfo(long ContactInfoID)
        {
            ContactInfoData objContactInfoData = null;

            try
            {
                using (var objConnect = GetDBConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendLine("SELECT * FROM Tbl_ContactInfo");
                    sbSQL.AppendLine("WHERE ContactInfoID=@ContactInfoID");
                    sbSQL.AppendLine("ORDER BY ContactInfoID DESC");

                    objContactInfoData = objConnect.Query<ContactInfoData>(sbSQL.ToString(), new { ContactInfoID = ContactInfoID }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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