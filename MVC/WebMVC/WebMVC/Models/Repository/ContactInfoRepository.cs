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

        public List<ContactInfoExData> GetContactInfoByCondition(QueryBaseData objQueryBaseData)
        {
            List<ContactInfoExData> liContactInfoExData = null;

            try
            {
                using (var objConnect = GetDBConnection())
                {
                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.AppendLine("SELECT *, (SELECT COUNT(1) FROM Tbl_ContactInfo) AS TotalCount FROM Tbl_ContactInfo");
                    sbSQL.AppendLine("WHERE 1=1");

                    #region [Query Condition]
                    if (objQueryBaseData.QueryParam.Any())
                    {
                        if (objQueryBaseData.QueryParam.ContainsKey("IsEnable"))
                        {
                            sbSQL.AppendLine("AND IsEnable=@IsEnable");
                        }
                        if (objQueryBaseData.QueryParam.ContainsKey("Name"))
                        {
                            sbSQL.AppendLine("AND Name LIKE @Name");
                        }
                        if (objQueryBaseData.QueryParam.ContainsKey("Nickname"))
                        {
                            sbSQL.AppendLine("AND Nickname LIKE @Nickname");
                        }
                    }
                    #endregion

                    #region [Order]
                    string strSort = objQueryBaseData.DataTableParam.OrderColumn + " " + objQueryBaseData.DataTableParam.OrderDir;
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
                    else
                    {
                        sbSQL.AppendLine("ORDER BY ContactInfoID ASC");
                    }
                    #endregion

                    #region[Paging]
                    if (null != objQueryBaseData.DataTableParam.PageStartRow && null != objQueryBaseData.DataTableParam.PageRowCnt)
                    {
                        sbSQL.AppendLine("OFFSET @Start ROWS FETCH NEXT @Length ROWS ONLY");
                    }
                    #endregion

                    liContactInfoExData = objConnect.Query<ContactInfoExData>(sbSQL.ToString(), new {
                        IsEnable = (objQueryBaseData.QueryParam.ContainsKey("IsEnable") ? objQueryBaseData.QueryParam["IsEnable"] : 0),
                        Name = (objQueryBaseData.QueryParam.ContainsKey("Name") ? "%" + objQueryBaseData.QueryParam["Name"] + "%" : string.Empty),
                        Nickname = (objQueryBaseData.QueryParam.ContainsKey("Nickname") ? "%" + objQueryBaseData.QueryParam["Nickname"] + "%" : string.Empty),
                        Sort = strSort ?? string.Empty,
                        Start = objQueryBaseData.DataTableParam.PageStartRow ?? 0,
                        Length = objQueryBaseData.DataTableParam.PageRowCnt ?? 0
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